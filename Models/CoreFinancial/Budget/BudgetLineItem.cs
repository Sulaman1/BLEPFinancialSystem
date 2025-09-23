namespace BLEPFinancialSystem.Models.CoreFinancial.Budget
{
    public class BudgetLineItem
    {
        public int Id { get; set; }
        public string Code { get; set; } // WB Chart of Accounts
        public string Description { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal RevisedAmount { get; set; }
        public decimal ActualExpenditure { get; set; }
        public decimal Commitments { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}
