namespace BLEPFinancialSystem.Models.ProjectManagement.MandE
{
    public class Indicator
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal BaselineValue { get; set; }
        public decimal TargetValue { get; set; }
        public List<IndicatorMeasurement> Measurements { get; set; }
    }
}
