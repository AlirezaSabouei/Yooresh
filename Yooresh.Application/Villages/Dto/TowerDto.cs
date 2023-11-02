using AutoMapper;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Application.Villages.Dto;

public class TowerDto : IMapFrom<Tower>
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