namespace OrderProcessingLib
{
    public class OrderProcessing
    {
        private readonly IEmailService _emailService;

        public OrderProcessing(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Ship(IOrder order)
        {
            order.Ship();
            _emailService.Send();
        }
    }
}