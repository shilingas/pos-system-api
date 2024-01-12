using Microsoft.AspNetCore.Mvc;
using pos_system.Workers.Roles;

namespace pos_system.Workers
{
    public interface IWorkerService
    {
        Task<WorkerDto> AddRoleToWorker(string workerId, string roleId);
        Task<WorkerModel> CreateWorker(WorkerPostRequestModel workerRequestModel);
        Task<bool> DeleteWorker(string id);
        Task<List<RoleModel>?> GetRolesOfWorker(string workerId);
        Task<WorkerDto?> GetWorkerById(string workerId);
        Task<List<WorkerDto>> GetWorkers();
        Task<bool> RemoveRole(string workerId, string roleId);
        Task<WorkerModel?> UpdateWorker(string id, WorkerPostRequestModel workerRequestModel);
        int validateInput(WorkerPostRequestModel model);
        int validateRolesAddition(string workerId, string roleId);
        int validateRolesDeletion(string workerId, string roleId);
        int roleValidation(string workerId, string roleId);

    }
}