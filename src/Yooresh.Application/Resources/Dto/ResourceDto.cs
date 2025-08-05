using Yooresh.Application.Account.Dto;
using Yooresh.Application.Common;
using Yooresh.Application.Common.Mappings;
using Yooresh.Domain.Entities.Resources;

namespace Yooresh.Application.Resources.Dto;

public class ResourceDto : BaseDto, IMapFrom<Resource>
{
    public required PlayerReferenceDto Player { get; set; }
    public ResourceTypeDto ResourceType { get; set; }
    public int AvailableAmount { get; set; }
    public int HarvestRatePerMinute { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
