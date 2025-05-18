using AutoMapper;
using Villages.Application.Common.Mappings;
using Villages.Domain.Common.ValueObjects;
using Villages.Domain.Core.DefenseBuildings.Entities;

namespace Villages.Application.Villages.Dto;

public class WallDto : IMapFrom<Wall>
{
    public string Name { get; set; }
    public Defense Defense { get; set; }
    public int Health { get; set; }
    public Resource RepairCost { get; set; }

    #region Upgradable

    public string UpgradeName { get; set; }
    public Resource UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public WallDto? Target { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public int Level { get; set; }

    #endregion

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Wall, WallDto>()
            .ReverseMap();
    }
}
