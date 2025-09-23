using System.Security.Cryptography;

namespace BLEPFinancialSystem.Models.CoreFinancial.Procurement
{
    public class ProcurementPlan
    {
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public ProcurementMethod Method { get; set; }
        public string Description { get; set; }
        public decimal EstimatedAmount { get; set; }
        public ProcurementStatus Status { get; set; }
        public List<Bid> Bids { get; set; }
    }
}
