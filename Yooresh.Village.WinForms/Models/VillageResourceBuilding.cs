namespace Yooresh.Client.WinForms.Models;

public class VillageResourceBuilding
{
    public Guid VillageId { get; set; }
    public Village Village { get; set; }
    public Guid ResourceBuildingId { get; set; }
    public ResourceBuilding ResourceBuilding { get; set; }
}