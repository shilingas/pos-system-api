namespace pos_system.Order.Bill
{
    public interface IBillService
    {
        Task<BillModel> getBill(string orderId);
    }
}