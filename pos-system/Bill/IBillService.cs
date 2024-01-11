
namespace pos_system.Bill
{
    public interface IBillService
    {
        Task<BillModel> getBill(string orderId);
    }
}