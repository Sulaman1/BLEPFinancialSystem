namespace BLEPFinancialSystem.Models.RequiredIntegrations
{
    public class SMSService
    {
        public async Task SendPaymentNotification(Beneficiary beneficiary, Payment payment)
        {
            // Integration with SMS gateway for beneficiary notifications
        }

        public async Task SendAlert(string phoneNumber, string message)
        {
            // For important alerts and notifications
        }
    }
}
