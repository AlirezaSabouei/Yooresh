using AutoMapper;
using System;
using System.Collections.Generic;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Buildings;
using Yooresh.Domain.Entities.Resources;
using Yooresh.Domain.Entities.Villages;
using Yooresh.Domain.ValueObjects;

namespace Yooresh.Application.Villages.Dto;

public class WallDto : IMapFrom<Wall>
{
    public string Name { get; set; }
    public Defense Defense { get; set; }
    public int Health { get; set; }
    public ResourceValueObject RepairCost { get; set; }

    #region Upgradable

    public string UpgradeName { get; set; }
    public ResourceValueObject UpgradeCost { get; set; }
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
