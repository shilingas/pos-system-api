namespace pos_system.Reservation
{
    public class ReservationPostRequestModel
    {
        public string? OrderId { get; set; }
        public string? ServiceId { get; set; }
        public string? StartDateTime { get; set; }
    }
}
