using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using pos_system.Order;
using pos_system.Roles;
using pos_system.Migrations;

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
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkers()
        {
            var workers = await _context.Workers
                                        .Include(w => w.WorkerRoles)
                                        .ThenInclude(wr => wr.Role)
                                        .ToListAsync();

            var workerDtos = workers.Select(worker => new WorkerDto
            {
                Id = worker.Id,
                Name = worker.Name,
                Email = worker.Email,
                Phone = worker.Phone,
                Username = worker.Username,
                Roles = worker.WorkerRoles.Select(wr => new RoleDto
                {
                    Id = wr.Role.Id,
                    Name = wr.Role.Name
                }).ToList()
            }).ToList();

            return Ok(workerDtos);
        }

        [HttpGet("{workerId}")]
        public async Task<ActionResult<WorkerDto>> GetWorkerById(string workerId)
        {
            var worker = await _context.Workers
                                    .Include(w => w.WorkerRoles)
                                    .ThenInclude(wr => wr.Role)
                                    .SingleOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
            {
                return NotFound("Toks darbuotojas nerastas!");
            }

            var workerDto = new WorkerDto
            {
                Id = worker.Id,
                Name = worker.Name,
                Email = worker.Email,
                Phone = worker.Phone,
                Username = worker.Username,
                Roles = worker.WorkerRoles.Select(wr => new RoleDto
                {
                    Id = wr.Role.Id,
                    Name = wr.Role.Name
                }).ToList()
            };

            return Ok(workerDto);
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

        [HttpPost("{workerId}/roles/{roleId}")]
        public async Task<ActionResult<WorkerDto>> AddRoleToWorker(string workerId, string roleId)
        {
            WorkerModel? worker = await _context.Workers.FindAsync(workerId);
            if (worker == null)
            {
                return NotFound("Darbuotojas su tokiu ID nerastas!");
            }

            RoleModel? role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                return NotFound("Rolė su tokiu ID nerasta!");
            }

            if (await _context.WorkerRoles.AnyAsync(wr => wr.WorkerId == workerId && wr.RoleId == roleId))
            {
                return BadRequest("Šis darbuotojas jau turi šią rolę!");
            }

            WorkerRole workerRole = new WorkerRole
            {
                Id = Guid.NewGuid().ToString(),
                WorkerId = workerId,
                RoleId = roleId,
            };

            _context.WorkerRoles.Add(workerRole);

            worker = await _context.Workers
                            .Include(w => w.WorkerRoles)
                            .ThenInclude(wr => wr.Role)
                            .FirstOrDefaultAsync(w => w.Id == workerId);

            WorkerDto workerDto = new WorkerDto
            {
                Id = worker.Id,
                Name = worker.Name,
                Email = worker.Email,
                Phone = worker.Phone,
                Username = worker.Username,
                Roles = worker.WorkerRoles.Select(wr => new RoleDto
                {
                    Id = wr.Role.Id,
                    Name = wr.Role.Name
                }).ToList()
            };

            await _context.SaveChangesAsync();
            return Ok(workerDto);


        }

        [HttpGet("{workerId}/roles")]
        public async Task<ActionResult<List<RoleModel>>> GetRolesOfWorker(string workerId)
        {
            var worker = await _context.Workers
                               .Include(w => w.WorkerRoles)
                               .ThenInclude(wr => wr.Role)
                               .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
            {
                return NotFound("Toks darbuotojas nerastas!");
            }

            var roles = worker.WorkerRoles
                              .Select(wr => wr.Role)
                              .ToList();

            return Ok(roles);
        }

        [HttpDelete("{workerId}/roles/{roleId}")]
        public async Task<ActionResult> RemoveRole(string workerId, string roleId)
        {
            WorkerModel? worker = await _context.Workers.FindAsync(workerId);
            if (worker == null)
            {
                return NotFound("Darbuotojas su tokiu ID nerastas");
            }

            RoleModel? role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                return NotFound("Rolė su tokiu ID nerasta");
            }

            WorkerRole? workerrole = await _context.WorkerRoles.FirstOrDefaultAsync(wr => wr.WorkerId == workerId && wr.RoleId == roleId);
            if (workerrole == null)
            {
                return NotFound("Pasirinkta rolė nėra priskirta pasirinktam vartotojui!");
            }

            _context.WorkerRoles.Remove(workerrole);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
