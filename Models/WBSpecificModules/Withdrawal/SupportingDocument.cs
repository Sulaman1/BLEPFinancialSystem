namespace BLEPFinancialSystem.Models.WBSpecificModules.Withdrawal
{
    public class SupportingDocument
    {
        public int Id { get; set; }
        public string DocumentType { get; set; } // Invoice, Receipt, etc.
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
