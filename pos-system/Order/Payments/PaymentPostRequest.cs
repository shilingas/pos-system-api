namespace pos_system.Order.Payments
{
    public class PaymentPostRequest
    {
        public string? OrderId { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
    }
}
