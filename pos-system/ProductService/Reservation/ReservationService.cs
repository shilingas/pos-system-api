using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Workers;

namespace pos_system.ProductService.Reservation
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
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == reservationModel.ServiceId);

            TimeSpan duration = service.Duration.HasValue ? TimeSpan.FromMinutes(service.Duration.Value) : TimeSpan.Zero;
            DateTime startDateTime = DateTime.Parse(reservationModel.StartDateTime).AddHours(-2);
            startDateTime = startDateTime.AddSeconds(-startDateTime.Second).AddMilliseconds(-startDateTime.Millisecond);

            DateTime endDateTime = startDateTime.Add(duration);

            ReservationModel reservation = new ReservationModel
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = reservationModel.OrderId,
                ServiceId = reservationModel.ServiceId,
                WorkerId = reservationModel.WorkerId,
                CustomerId = reservationModel.CustomerId,
                StartDateTime = startDateTime,
                Duration = duration
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
                    updated.StartDateTime = DateTime.Parse(reservationModel.StartDateTime);
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;
        }

        public async Task<IEnumerable<TimeSpan>> GetFreeTimesOfWorker(string workerId, DateTime date)
        {
            var workDayStart = new TimeSpan(9, 0, 0);
            var workDayEnd = new TimeSpan(17, 0, 0);

            var reservations = await _context.Reservations
                                        .Where(r => r.WorkerId == workerId && r.StartDateTime.Value.Date == date.Date)
                                        .ToListAsync();

            var allSlots = new List<TimeSpan>();
            for (var time = workDayStart; time < workDayEnd; time = time.Add(TimeSpan.FromMinutes(30)))
            {
                allSlots.Add(time);
            }

            foreach (var reservation in reservations)
            {
                var reservationEnd = reservation.StartDateTime.Value.TimeOfDay.Add(reservation.Duration.Value);
                reservationEnd = reservationEnd.Add(-TimeSpan.FromMinutes(1));

                allSlots.RemoveAll(slot => slot >= reservation.StartDateTime.Value.TimeOfDay && slot < reservationEnd);
            }

            return allSlots;
        }

        public int validateCreation(ReservationPostRequestModel reservationModel)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == reservationModel.ServiceId);
            if (service == null)
            {
                return 0;
            }


            if (!DateTime.TryParse(reservationModel.StartDateTime, out DateTime startDateTime))
            {
                return 3;
            }

            TimeSpan duration = service.Duration.HasValue ? TimeSpan.FromMinutes(service.Duration.Value) : TimeSpan.Zero;
            startDateTime = startDateTime.AddSeconds(-startDateTime.Second).AddMilliseconds(-startDateTime.Millisecond);

            if (startDateTime.Minute != 0 && startDateTime.Minute != 30)
            {
                return 1;
            }

            WorkerModel? worker = _context.Workers.Find(reservationModel.WorkerId);
            if (worker == null)
            {
                return 4;
            }

            DateTime endDateTime = startDateTime.Add(duration);

            var reservations = _context.Reservations
                .Where(r => r.WorkerId == reservationModel.WorkerId)
                .ToList();

            var isWorkerBooked = reservations.Any(r =>
                r.StartDateTime < endDateTime &&
                r.StartDateTime.Value.Add(r.Duration.Value) > startDateTime);

            if (isWorkerBooked)
            {
                return 2;
            }

            return -1;
        }
    }
}
