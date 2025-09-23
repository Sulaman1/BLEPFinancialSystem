namespace BLEPFinancialSystem.Models.WBSpecificModules.Withdrawal
{
    public class WithdrawalApplication
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public decimal AmountRequested { get; set; }
        public string Purpose { get; set; }
        public WBApplicationStatus Status { get; set; }
        public List<SupportingDocument> Documents { get; set; }
    }
}
