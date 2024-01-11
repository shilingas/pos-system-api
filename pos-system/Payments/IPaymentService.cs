
namespace pos_system.Payments
{
    public interface IPaymentService
    {
        Task<PaymentModel?> CreatePayment(PaymentPostRequest paymentInput);
        Task<List<PaymentModel>?> GetAllUserPayments(string userId);
        Task<PaymentModel[]> GetPayments();
        Task<PaymentModel?> GetOrderPayment(string orderId);
        Task<PaymentModel?> GetPayment(string paymentId);
        Task<PaymentModel> UpdatePayment(string paymentId, PaymentPostRequest payment);
        Task<bool> DeletePayment(string paymentId);
    }
}