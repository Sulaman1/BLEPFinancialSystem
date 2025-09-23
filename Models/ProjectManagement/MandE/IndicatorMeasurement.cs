namespace BLEPFinancialSystem.Models.ProjectManagement.MandE
{
    public class IndicatorMeasurement
    {
        public int Id { get; set; }
        public DateTime MeasurementDate { get; set; }
        public decimal ActualValue { get; set; }
        public string DataSource { get; set; }
        public string Remarks { get; set; }
    }
}
