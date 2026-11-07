using MongoDB.Driver;
using Yooresh.Domain.Entities.Accounts;
using Yooresh.Domain.Entities.DefensiveBuildings;
using Yooresh.Domain.Entities.DefensiveBuildingUpgrades;
using Yooresh.Domain.Entities.Factions;
using Yooresh.Domain.Entities.ResourceBuildings;
using Yooresh.Domain.Entities.ResourceBuildingUpgrades;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Infrastructure.Persistence;

/// <summary>
/// Creates the indexes and the reference data (factions and upgrade catalogues) that used to be
/// created by EF Core migrations and <c>IEntityTypeConfiguration</c> seed data.
/// The operation is idempotent: a collection that already has documents is left untouched.
/// </summary>
public static class MongoDbInitializer
{
    private static readonly string IdDirectory = Path.Combine(
        AppContext.BaseDirectory, "Persistence", "Seeding", "Id");

    public static async Task InitializeAsync(IMongoDatabase database, CancellationToken cancellationToken = default)
    {
        await CreateIndexesAsync(database, cancellationToken);
        await SeedFactionsAsync(database, cancellationToken);
        await SeedResourceBuildingUpgradesAsync(database, cancellationToken);
        await SeedDefensiveBuildingUpgradesAsync(database, cancellationToken);
    }

    private static async Task CreateIndexesAsync(IMongoDatabase database, CancellationToken cancellationToken)
    {
        var players = database.GetCollection<Player>(nameof(Player));
        await players.Indexes.CreateOneAsync(
            new CreateIndexModel<Player>(
                Builders<Player>.IndexKeys.Ascending(a => a.Email),
                new CreateIndexOptions { Unique = true, Name = "UX_Player_Email" }),
            cancellationToken: cancellationToken);

        await players.Indexes.CreateOneAsync(
            new CreateIndexModel<Player>(
                Builders<Player>.IndexKeys.Ascending("RefreshTokens.Token"),
                new CreateIndexOptions { Name = "IX_Player_RefreshTokens_Token" }),
            cancellationToken: cancellationToken);

        await CreatePlayerIndexAsync<Resource>(database, nameof(Resource), cancellationToken);
        await CreatePlayerIndexAsync<ResourceBuilding>(database, nameof(ResourceBuilding), cancellationToken);
        await CreatePlayerIndexAsync<DefensiveBuilding>(database, nameof(DefensiveBuilding), cancellationToken);

        var resourceUpgrades = database.GetCollection<ResourceBuildingUpgrade>(nameof(ResourceBuildingUpgrade));
        await resourceUpgrades.Indexes.CreateOneAsync(
            new CreateIndexModel<ResourceBuildingUpgrade>(
                Builders<ResourceBuildingUpgrade>.IndexKeys
                    .Ascending(a => a.ResourceBuildingType)
                    .Ascending(a => a.Level),
                new CreateIndexOptions { Name = "IX_ResourceBuildingUpgrade_Type_Level" }),
            cancellationToken: cancellationToken);

        var defensiveUpgrades = database.GetCollection<DefensiveBuildingUpgrade>(nameof(DefensiveBuildingUpgrade));
        await defensiveUpgrades.Indexes.CreateOneAsync(
            new CreateIndexModel<DefensiveBuildingUpgrade>(
                Builders<DefensiveBuildingUpgrade>.IndexKeys
                    .Ascending(a => a.DefensiveBuildingType)
                    .Ascending(a => a.Level),
                new CreateIndexOptions { Name = "IX_DefensiveBuildingUpgrade_Type_Level" }),
            cancellationToken: cancellationToken);
    }

    private static async Task CreatePlayerIndexAsync<TEntity>(
        IMongoDatabase database, string collectionName, CancellationToken cancellationToken)
        where TEntity : Domain.Entities.BaseEntity
    {
        var collection = database.GetCollection<TEntity>(collectionName);

        await collection.Indexes.CreateOneAsync(
            new CreateIndexModel<TEntity>(
                Builders<TEntity>.IndexKeys.Ascending("Player._id"),
                new CreateIndexOptions { Name = "IX_Player_Id" }),
            cancellationToken: cancellationToken);
    }

    private static async Task SeedFactionsAsync(IMongoDatabase database, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<Faction>(nameof(Faction));
        if (await HasDocumentsAsync(collection, cancellationToken))
        {
            return;
        }

        var factions = new List<Faction>
        {
            CreateFaction("Orc", "012FC556-7788-42B7-82F7-F093D37EC517"),
            CreateFaction("Human", "52559F4B-4052-4438-B5B9-9BAC92DC75FB"),
            CreateFaction("Undead", "56D9E842-3C04-4741-9CDA-4BFE38AA1F57"),
            CreateFaction("Elf", "1A060E07-FD38-4C1B-96E8-B8DAEE50421E")
        };

        await collection.InsertManyAsync(factions, cancellationToken: cancellationToken);
    }

    private static Faction CreateFaction(string name, string id)
    {
        var advantages = new List<string> { "Advantage1", "Advantage2" };
        var disadvantages = new List<string> { "Disadvantage1", "Disadvantage2" };

        return new Faction(name, advantages, disadvantages, new Guid(id));
    }

    private static async Task SeedResourceBuildingUpgradesAsync(
        IMongoDatabase database, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<ResourceBuildingUpgrade>(nameof(ResourceBuildingUpgrade));
        if (await HasDocumentsAsync(collection, cancellationToken))
        {
            return;
        }

        var upgrades = new List<ResourceBuildingUpgrade>();
        AddResourceBuildingUpgrades(upgrades, ResourceBuildingType.GoldMine, "GoldMine.txt", "Gold mine");
        AddResourceBuildingUpgrades(upgrades, ResourceBuildingType.LumberMill, "LumberMill.txt", "Lumber mill");
        AddResourceBuildingUpgrades(upgrades, ResourceBuildingType.StoneMine, "StoneMine.txt", "Stone mine");
        AddResourceBuildingUpgrades(upgrades, ResourceBuildingType.Farm, "Farm.txt", "Farm");

        await collection.InsertManyAsync(upgrades, cancellationToken: cancellationToken);
    }

    private static void AddResourceBuildingUpgrades(
        List<ResourceBuildingUpgrade> upgrades,
        ResourceBuildingType resourceBuildingType,
        string idFileName,
        string displayName)
    {
        var ids = ReadIds(idFileName);

        for (var level = 20; level >= 0; level--)
        {
            upgrades.Add(new ResourceBuildingUpgrade
            {
                Id = ids[level],
                Level = level,
                ResourceBuildingType = resourceBuildingType,
                UpgradeName = $"{displayName} level {level + 1}",
                UpgradeCost = level == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(level ^ 2 * 10, level ^ 2 * 10, level ^ 2 * 10, level ^ 2 * 10),
                BonusHarvestRatePerMinute = level * 5,
                UpgradeDuration = new TimeSpan(0, level * level * level, 0),
                NeedBuilderForUpgrade = true
            });
        }
    }

    private static async Task SeedDefensiveBuildingUpgradesAsync(
        IMongoDatabase database, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<DefensiveBuildingUpgrade>(nameof(DefensiveBuildingUpgrade));
        if (await HasDocumentsAsync(collection, cancellationToken))
        {
            return;
        }

        var upgrades = new List<DefensiveBuildingUpgrade>();
        AddDefensiveBuildingUpgrades(upgrades, DefensiveBuildingType.Tower, "Tower.txt", "Tower");
        AddDefensiveBuildingUpgrades(upgrades, DefensiveBuildingType.Wall, "Wall.txt", "Wall");

        await collection.InsertManyAsync(upgrades, cancellationToken: cancellationToken);
    }

    private static void AddDefensiveBuildingUpgrades(
        List<DefensiveBuildingUpgrade> upgrades,
        DefensiveBuildingType defensiveBuildingType,
        string idFileName,
        string displayName)
    {
        var ids = ReadIds(idFileName);

        for (var level = 20; level >= 0; level--)
        {
            upgrades.Add(new DefensiveBuildingUpgrade
            {
                Id = ids[level],
                Level = level,
                DefensiveBuildingType = defensiveBuildingType,
                UpgradeName = $"{displayName} level {level + 1}",
                UpgradeCost = level == 1
                    ? new ResourceValueObject(0, 0, 0, 0)
                    : new ResourceValueObject(level ^ 2 * 10, level ^ 2 * 10, level ^ 2 * 10, level ^ 2 * 10),
                RepairCost = new ResourceValueObject(0, 0, 0, 0),
                BonusAttack = level * 5,
                BonusDefense = level * 5,
                BonusHitPoint = level * 5,
                UpgradeDuration = new TimeSpan(0, level * level * level, 0),
                NeedBuilderForUpgrade = true
            });
        }
    }

    private static List<Guid> ReadIds(string fileName)
    {
        var path = Path.Combine(IdDirectory, fileName);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                $"Seed id file '{fileName}' was not found. Expected it at '{path}'.", path);
        }

        return File.ReadLines(path).Select(Guid.Parse).ToList();
    }

    private static async Task<bool> HasDocumentsAsync<TEntity>(
        IMongoCollection<TEntity> collection, CancellationToken cancellationToken)
    {
        var count = await collection.CountDocumentsAsync(
            FilterDefinition<TEntity>.Empty, cancellationToken: cancellationToken);

        return count > 0;
    }
}
