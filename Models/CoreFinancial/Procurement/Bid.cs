using System.Net.NetworkInformation;

namespace BLEPFinancialSystem.Models.CoreFinancial.Procurement
{
    public class Bid
    {
        public int Id { get; set; }
        public string BidderName { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public BidStatus Status { get; set; }
    }
}
