using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using pos_system.Order;

namespace pos_system.Workers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly PosContext _context;
        public WorkerController(PosContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public async Task<WorkerModel[]> GetWorkers()
        {
            WorkerModel[] workers = await _context.Workers.ToArrayAsync();

            return workers;
        }

        [HttpPost]
        public async Task<ActionResult<WorkerModel[]>> CreateWorker([FromBody] WorkerPostRequestModel workerRequestModel)
        {
            string phonePattern = @"^\+?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            string usernamePattern = @"^[a-zA-Z0-9]([._](?![._])|[a-zA-Z0-9]){2,18}[a-zA-Z0-9]$";

            if (workerRequestModel != null)
            {
                if (!Regex.IsMatch(workerRequestModel.Email, emailPattern))
                {
                    return BadRequest("Netinkamas el.pašto formatas");
                }

                if (!Regex.IsMatch(workerRequestModel.Phone, phonePattern))
                {
                    return BadRequest("Netinkamas telefono formatas");
                }

                if (!Regex.IsMatch(workerRequestModel.Username, usernamePattern))
                {
                    return BadRequest("Netinkamas vartotojo vardo formatas");
                }
            }
            else
            {
                return BadRequest("Būtina užpildyti duomenis");
            }

            WorkerModel? existingWorker = await _context.Workers.FirstOrDefaultAsync(w => w.Username == workerRequestModel.Username);
            if (existingWorker != null)
            {
                return BadRequest("Vartotojo vardas jau užimtas");
            }

            WorkerModel worker = new WorkerModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = workerRequestModel.Name,
                Email = workerRequestModel.Email,
                Phone = workerRequestModel.Phone,
                Password = workerRequestModel.Password,
                Username = workerRequestModel.Username,
            };

            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
            return Ok(worker);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkerModel>> UpdateWorker(string id, [FromBody] WorkerPostRequestModel workerRequestModel)
        {
            string phonePattern = @"^\+?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            string usernamePattern = @"^[a-zA-Z0-9]([._](?![._])|[a-zA-Z0-9]){2,18}[a-zA-Z0-9]$";

            WorkerModel? worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            if (!Regex.IsMatch(workerRequestModel.Email, emailPattern))
            {
                return BadRequest("Netinkamas el.pašto formatas");
            }

            if (!Regex.IsMatch(workerRequestModel.Phone, phonePattern))
            {
                return BadRequest("Netinkamas telefono formatas");
            }

            if (!Regex.IsMatch(workerRequestModel.Username, usernamePattern))
            {
                return BadRequest("Netinkamas vartotojo vardo formatas");
            }

            worker.Email = workerRequestModel.Email;
            worker.Phone = workerRequestModel.Phone;
            worker.Password = workerRequestModel.Password;
            worker.Username = workerRequestModel.Username;

            await _context.SaveChangesAsync();
            return Ok(worker);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorker(string id)
        {
            WorkerModel? worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
