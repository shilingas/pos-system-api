namespace pos_system.ProductService.Reservation
{
    public class ReservationPostRequestModel
    {
        public string? OrderId { get; set; }
        public string? ServiceId { get; set; }
        public string? WorkerId { get; set; }
        public string? CustomerId { get; set; }
        public string? StartDateTime { get; set; }
    }
}
