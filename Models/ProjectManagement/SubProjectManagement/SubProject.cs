namespace BLEPFinancialSystem.Models.ProjectManagement.SubProjectManagement
{
    public class SubProject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public decimal ApprovedBudget { get; set; }
        public SubProjectStatus Status { get; set; }
        public List<SubProjectComponent> Components { get; set; }
    }
}
