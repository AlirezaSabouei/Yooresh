namespace Eloy.DTO;

public class ResourceBuilding
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    #region Upgrade

    public string UpgradeName { get; set; } = string.Empty;
    public ResourceDto UpgradeCost { get; set; } = new();
    public TimeSpan UpgradeDuration { get; set; } = new();
    public Guid? TargetId { get; set; }
    public ResourceBuilding? Target { get; set; } = new();
    public bool NeedBuilderForUpgrade { get; set; }
    public int Level { get; set; } = new();

    #endregion

    #region Resource Production
    public ResourceDto HourlyProduction { get; set; } = new();

    #endregion
}