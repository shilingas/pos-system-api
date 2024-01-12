using pos_system.ProductService.Services;
using System.Diagnostics.CodeAnalysis;

namespace pos_system.ProductService.Reservation
{
    public class ReservationModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? ServiceId { get; set; }
        public ServiceModel? Service { get; set; }
        public string? WorkerId { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public TimeSpan? Duration { get; set; }

    }
}
