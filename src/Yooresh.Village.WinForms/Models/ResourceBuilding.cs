namespace Yooresh.Client.WinForms.Models;

public class ResourceBuilding
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    #region Upgrade

    public string UpgradeName { get; set; }
    public Resource UpgradeCost { get; set; }
    public TimeSpan UpgradeDuration { get; set; }
    public Guid? TargetId { get; set; }
    public ResourceBuilding? Target { get; set; }
    public bool NeedBuilderForUpgrade { get; set; }
    public int Level { get; set; }

    #endregion

    #region Resource Production
    public Resource HourlyProduction { get; set; }

    #endregion
}