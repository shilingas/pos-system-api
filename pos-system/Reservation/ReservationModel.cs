using System.Diagnostics.CodeAnalysis;

namespace pos_system.Reservation
{
    public class ReservationModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? OrderId { get; set; }

        public string? ServiceId { get; set; }
        public string? StartDateTime { get; set; }
    }
}
