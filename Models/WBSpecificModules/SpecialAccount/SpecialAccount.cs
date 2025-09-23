namespace BLEPFinancialSystem.Models.WBSpecificModules.SpecialAccount
{
    public class SpecialAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal ReconciliationBalance { get; set; }
        public List<SpecialAccountTransaction> Transactions { get; set; }
    }
}
