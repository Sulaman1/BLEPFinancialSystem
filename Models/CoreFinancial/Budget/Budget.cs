namespace BLEPFinancialSystem.Models.CoreFinancial.Budget
{
    public class Budget
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int FiscalYear { get; set; }
        public BudgetStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<BudgetLineItem> LineItems { get; set; }
    }
}
