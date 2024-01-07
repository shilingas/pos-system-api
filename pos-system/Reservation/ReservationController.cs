using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Order;
using pos_system.Services;

namespace pos_system.Reservation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly PosContext _context;

        public ReservationController(PosContext _context)
        {
            this._context = _context;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ReservationModel>> CreateReservation([FromBody] ReservationPostRequestModel reservationPostRequest)
        {
            ReservationModel reservation = new ReservationModel
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = reservationPostRequest.OrderId,
                ServiceId = reservationPostRequest.ServiceId,
                StartDateTime = reservationPostRequest.StartDateTime,
            };
            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationModel>?> GetReservation(string id)
        {
            ReservationModel? reservation = new ReservationModel();
            reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                return reservation;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<ReservationModel[]> GetAllReservations()
        {
            ReservationModel[] reservations = new ReservationModel[0];
            if (_context != null)
            {
                reservations = await _context.Reservations.ToArrayAsync();
            }

            return reservations;
        }
        [HttpDelete("{id}")]
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
        [HttpPut("{reservationId}")]
        public async Task<ReservationModel?> UpdateReservation(string reservationId, ReservationUpdateModel reservationUpdateModel)
        {
            ReservationModel? updated = new ReservationModel();
            if (_context != null)
            {
                updated = await _context.Reservations.SingleOrDefaultAsync(res => res.Id == reservationId);
                if (updated == null)
                {
                    return null;
                }
                if (reservationUpdateModel.ServiceId != null)
                {
                    updated.ServiceId = reservationUpdateModel.ServiceId;
                }
                if (reservationUpdateModel.OrderId != null)
                {
                    updated.OrderId = reservationUpdateModel.OrderId;
                }
                if (reservationUpdateModel.StartDateTime != null)
                {
                    updated.StartDateTime = reservationUpdateModel.StartDateTime;
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;

        }
    }
}
