namespace BLEPFinancialSystem.Models.CoreFinancial.Contract
{
    public class Contract
    {
        public int Id { get; set; }
        public string ContractNo { get; set; }
        public string Contractor { get; set; }
        public string Description { get; set; }
        public decimal ContractAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ContractMilestone> Milestones { get; set; }
        public List<ContractPayment> Payments { get; set; }
    }
}
