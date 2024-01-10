using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;

namespace pos_system.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly PosContext _context;
        public ReservationService(PosContext context)
        {
            _context = context;
        }
        public async Task<ReservationModel?> CreateReservation(ReservationPostRequestModel reservationModel)
        {
            ReservationModel reservation = new ReservationModel
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = reservationModel.OrderId,
                ServiceId = reservationModel.ServiceId,
                StartDateTime = reservationModel.StartDateTime,
            };
            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<bool> DeleteReservation(string id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ReservationModel[]> GetAllReservations()
        {
            ReservationModel[] reservations = new ReservationModel[0];
            if (_context != null)
            {
                reservations = await _context.Reservations.ToArrayAsync();
            }

            return reservations;
        }

        public async Task<ReservationModel?> GetReservation(string id)
        {
            ReservationModel? reservation = new ReservationModel();
            reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                return reservation;
            }
            else
            {
                return null;
            }
        }

        public async Task<ReservationModel?> UpdateReservation(string reservationId, ReservationPostRequestModel reservationModel)
        {
            ReservationModel? updated = new ReservationModel();
            if (_context != null)
            {
                updated = await _context.Reservations.SingleOrDefaultAsync(res => res.Id == reservationId);
                if (updated == null)
                {
                    return null;
                }
                if (reservationModel.ServiceId != null)
                {
                    updated.ServiceId = reservationModel.ServiceId;
                }
                if (reservationModel.OrderId != null)
                {
                    updated.OrderId = reservationModel.OrderId;
                }
                if (reservationModel.StartDateTime != null)
                {
                    updated.StartDateTime = reservationModel.StartDateTime;
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;
        }
    }
}
