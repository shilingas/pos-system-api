
namespace pos_system.Order.Tips
{
    public interface ITipService
    {
        Task<OrderModel?> AddTip(string orderId, TipRequestModel tip);
    }
}