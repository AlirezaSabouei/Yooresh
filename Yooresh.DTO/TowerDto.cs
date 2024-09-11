using AutoMapper;

namespace Eloy.DTO;

public class TowerDto : IMapFrom<Tower>
{
    public string Name { get; set; }
    public Defense Defense { get; set; }
    public int Health { get; set; }
    public ResourceDto RepairCost { get; set; }

    #region Upgradable

    public string UpgradeName { get; set; }
    public ResourceDto UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public TowerDto? Target { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public int Level { get; set; }

    #endregion
    
    public int TroopCapacity { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tower, TowerDto>()
            .ReverseMap();
    }
}