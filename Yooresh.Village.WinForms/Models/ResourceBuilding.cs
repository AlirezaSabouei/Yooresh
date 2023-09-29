namespace Yooresh.Client.WinForms.Models
{
    public class ResourceBuilding
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan UpgradeDuration { get; set; }
        public Resource UpgradeCost { get; set; }

        public Guid? RequirementId { get; set; }
        public ResourceBuilding? Requirement { get; set; }
        public ProductionType ProductionType { get; set; }
        public Resource HourlyProduction { get; set; }
    }
}