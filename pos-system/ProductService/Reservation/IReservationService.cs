using pos_system.Customers;

namespace pos_system.ProductService.Reservation
{
    public interface IReservationService
    {
        Task<ReservationModel[]> GetAllReservations();
        Task<ReservationModel?> CreateReservation(ReservationPostRequestModel reservationModel);
        Task<ReservationModel?> GetReservation(string id);
        Task<ReservationModel?> UpdateReservation(string reservationId, ReservationPostRequestModel reservationModel);
        Task<bool> DeleteReservation(string id);
        Task<IEnumerable<TimeSpan>> GetFreeTimesOfWorker(string workerId, DateTime date);
        int validateCreation(ReservationPostRequestModel reservationModel);
    }
}
