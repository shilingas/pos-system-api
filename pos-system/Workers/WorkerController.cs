using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using pos_system.Order;
using pos_system.Roles;
using pos_system.Migrations;
using System.Data.Entity;

namespace pos_system.Workers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;
        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkerDto>>> GetWorkers()
        {
            var workers = await _workerService.GetWorkers();

            return Ok(workers);
        }

        [HttpGet("{workerId}")]
        public async Task<ActionResult<WorkerDto>> GetWorkerById(string workerId)
        {
            var worker = await _workerService.GetWorkerById(workerId);

            if (worker == null)
            {
                return NotFound("Toks darbuotojas nerastas!");
            }

            return Ok(worker);
        }

        [HttpPost]
        public async Task<ActionResult<WorkerModel[]>> CreateWorker([FromBody] WorkerPostRequestModel workerRequestModel)
        {
            int validation = _workerService.validateInput(workerRequestModel);   

            if (validation == 0)
            {
                return BadRequest("Būtina įvesti duomenis!");
            } else if (validation == 1)
            {
                return BadRequest("Neteisingas el.pašto formatas");
            } else if (validation == 2)
            {
                return BadRequest("Neteisingas telefono formatas");
            } else if (validation == 3)
            {
                return BadRequest("Neteisingas vartotojo vardo formatas");
            } else if (validation == 4)
            {
                return BadRequest("Toks vartotojo vardas jau užimtas");
            } else
            {
                WorkerModel worker = await _workerService.CreateWorker(workerRequestModel);
                return Ok(worker);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkerModel>> UpdateWorker(string id, [FromBody] WorkerPostRequestModel workerRequestModel)
        {
            int validation = _workerService.validateInput(workerRequestModel);
            if (validation == 0)
            {
                return BadRequest("Būtina įvesti duomenis!");
            }
            else if (validation == 1)
            {
                return BadRequest("Neteisingas el.pašto formatas");
            }
            else if (validation == 2)
            {
                return BadRequest("Neteisingas telefono formatas");
            }
            else if (validation == 3)
            {
                return BadRequest("Neteisingas vartotojo vardo formatas");
            }
            else if (validation == 4)
            {
                return BadRequest("Toks vartotojo vardas jau užimtas");
            }
            else
            {
                WorkerModel? worker = await _workerService.UpdateWorker(id, workerRequestModel);
                if (worker == null)
                {
                    return NotFound("Toks vartotojas nerastas");
                }

                return Ok(worker);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorker(string id)
        {
            bool worker = await _workerService.DeleteWorker(id);
            if (worker == false)
            {
                return NotFound("Vartotojas nerastas");
            }

            return Ok();
        }

        [HttpPost("{workerId}/roles/{roleId}")]
        public async Task<ActionResult<WorkerDto>> AddRoleToWorker(string workerId, string roleId)
        {
            int validation = _workerService.validateRolesAddition(workerId, roleId);
            if (validation == 0)
            {
                return NotFound("Tokio vartotojo nėra!");
            }
            else if (validation == 1)
            {
                return NotFound("Tokios rolės nėra!");
            }
            else if (validation == 2)
            {
                return BadRequest("Ši rolė jau priskirta šiam vartotojui");
            }

            WorkerDto workerDto = await _workerService.AddRoleToWorker(workerId, roleId);
            return Ok(workerDto);


        }

        [HttpGet("{workerId}/roles")]
        public async Task<ActionResult<List<RoleModel>?>> GetRolesOfWorker(string workerId)
        {
            var roles = await _workerService.GetRolesOfWorker(workerId);

            if (roles == null)
            {
                return NotFound("Toks darbuotojas nerastas!");
            }

            return Ok(roles);
        }

        [HttpDelete("{workerId}/roles/{roleId}")]
        public async Task<ActionResult> RemoveRole(string workerId, string roleId)
        {
            int validation = _workerService.validateRolesDeletion(workerId, roleId);
            if (validation == 0)
            {
                NotFound("Toks vartotojas nerastas");
            }
            else if (validation == 1)
            {
                NotFound("Tokia rolė nerasta");
            }
            else if (validation == 2)
            {
                BadRequest("Šis darbuotojas neturi šios rolės");
            }

            bool worker = await _workerService.RemoveRole(workerId, roleId);

            return Ok();
        }

    }
}
