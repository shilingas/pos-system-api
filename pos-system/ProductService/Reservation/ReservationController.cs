using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Customers;
using pos_system.Order;
using pos_system.ProductService.Services;

namespace pos_system.ProductService.Reservation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly PosContext _context;
        private readonly IReservationService _reservationService;

        public ReservationController(PosContext _context, IReservationService reservationService)
        {
            this._context = _context;
            _reservationService = reservationService;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ReservationModel>> CreateReservation([FromBody] ReservationPostRequestModel reservationPostRequest)
        {
            ReservationModel? coupon = await _reservationService.CreateReservation(reservationPostRequest);
            if (coupon == null)
            {
                return BadRequest();
            }
            return Ok(coupon);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationModel>?> GetReservation(string id)
        {
            ReservationModel? reservation = await _reservationService.GetReservation(id);
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
            return await _reservationService.GetAllReservations();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(string id)
        {
            bool isDeleted = await _reservationService.DeleteReservation(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("{reservationId}")]
        public async Task<ActionResult<ReservationModel?>> UpdateReservation(string reservationId, ReservationPostRequestModel reservationUpdateModel)
        {
            ReservationModel? reservation = await _reservationService.UpdateReservation(reservationId, reservationUpdateModel);
            if (reservation != null)
            {
                return Ok(reservation);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("{workerId}/{date}")]
        public async Task<ActionResult<IEnumerable<TimeSpan>>> GetFreeTimesOfWorker(string workerId, DateTime date)
        {
            var freeTimes = await _reservationService.GetFreeTimesOfWorker(workerId, date);
            if (freeTimes == null)
            {
                return BadRequest();
            }
            return Ok(freeTimes);

        }
    }
}
