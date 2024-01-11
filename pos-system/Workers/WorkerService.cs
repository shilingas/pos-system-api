using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Order;
using pos_system.Roles;
using System.Text.RegularExpressions;

namespace pos_system.Workers
{
    public class WorkerService : IWorkerService
    {
        private readonly PosContext _context;
        public WorkerService(PosContext _context)
        {
            this._context = _context;
        }

        public async Task<List<WorkerDto>> GetWorkers()
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

            return workerDtos;
        }

        public async Task<WorkerDto?> GetWorkerById(string workerId)
        {
            var worker = await _context.Workers
                                    .Include(w => w.WorkerRoles)
                                    .ThenInclude(wr => wr.Role)
                                    .SingleOrDefaultAsync(w => w.Id == workerId);

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

            return workerDto;
        }

        public async Task<WorkerModel> CreateWorker(WorkerPostRequestModel workerRequestModel)
        {
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
            return worker;
        }

        public async Task<WorkerModel?> UpdateWorker(string id, WorkerPostRequestModel workerRequestModel)
        {
            WorkerModel? worker = await _context.Workers.FindAsync(id);

            worker.Email = workerRequestModel.Email;
            worker.Phone = workerRequestModel.Phone;
            worker.Password = workerRequestModel.Password;
            worker.Username = workerRequestModel.Username;

            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<bool> DeleteWorker(string id)
        {
            WorkerModel? worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return false;
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WorkerDto> AddRoleToWorker(string workerId, string roleId)
        {
            WorkerRole workerRole = new WorkerRole
            {
                Id = Guid.NewGuid().ToString(),
                WorkerId = workerId,
                RoleId = roleId,
            };

            _context.WorkerRoles.Add(workerRole);

            WorkerModel? worker = _context.Workers.Find(workerId);
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
            return workerDto;


        }

        public async Task<List<RoleModel>?> GetRolesOfWorker(string workerId)
        {
            var worker = await _context.Workers
                               .Include(w => w.WorkerRoles)
                               .ThenInclude(wr => wr.Role)
                               .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
            {
                return null;
            }

            var roles = worker.WorkerRoles
                              .Select(wr => wr.Role)
                              .ToList();

            return roles;
        }

        public async Task<bool> RemoveRole(string workerId, string roleId)
        {
            WorkerRole? workerrole = _context.WorkerRoles.FirstOrDefault(wr => wr.WorkerId == workerId && wr.RoleId == roleId);
            if (workerrole == null)
            {
                return false;
            }

            _context.WorkerRoles.Remove(workerrole);
            await _context.SaveChangesAsync();

            return true;
        }

        public int validateInput(WorkerPostRequestModel model)
        {
            string phonePattern = @"^\+?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            string usernamePattern = @"^[a-zA-Z0-9]([._](?![._])|[a-zA-Z0-9]){2,18}[a-zA-Z0-9]$";

            if (model != null)
            {
                if (!Regex.IsMatch(model.Email, emailPattern))
                {
                    return 1;
                }

                if (!Regex.IsMatch(model.Phone, phonePattern))
                {
                    return 2;
                }

                if (!Regex.IsMatch(model.Username, usernamePattern))
                {
                    return 3;
                }
            }
            else
            {
                return 0;
            }

            WorkerModel? existingWorker = _context.Workers.FirstOrDefault(w => w.Username == model.Username);
            if (existingWorker != null)
            {
                return 4;
            }

            return -1;
        }

        public int validateRolesAddition(string workerId, string roleId)
        {
            int validation = roleValidation(workerId, roleId);

            if (validation != -1)
            {
                return validation;
            }

            if (_context.WorkerRoles.Any(wr => wr.WorkerId == workerId && wr.RoleId == roleId))
            {
                return 2;
            }
            return -1;
        }

        public int validateRolesDeletion(string workerId, string roleId)
        {
            int validation = roleValidation(workerId, roleId);

            if (validation != -1)
            {
                return validation;
            }

            WorkerRole? workerrole = _context.WorkerRoles.FirstOrDefault(wr => wr.WorkerId == workerId && wr.RoleId == roleId);
            if (workerrole == null)
            {
                return 2;
            }

            return -1;
        }

        public int roleValidation(string workerId, string roleId)
        {
            WorkerModel? worker = _context.Workers.Find(workerId);
            if (worker == null)
            {
                return 0;
            }

            RoleModel? role = _context.Roles.Find(roleId);
            if (role == null)
            {
                return 1;
            }

            return -1;
        }

    }
}
