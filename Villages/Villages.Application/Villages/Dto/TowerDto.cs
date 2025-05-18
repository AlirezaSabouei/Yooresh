using AutoMapper;
using Villages.Application.Common.Mappings;
using Villages.Domain.Core.DefenseBuildings.Entities;
using Villages.Domain.Core.Entities;

namespace Villages.Application.Villages.Dto;

public class TowerDto : IMapFrom<Tower>
{
    public Guid Id { get; set; }
    public Guid TowerId { get; set; }
    public TowerDto Tower { get; set; }

    public Guid VillageId { get; set; }
    public VillageDto Village { get; set; }
}
