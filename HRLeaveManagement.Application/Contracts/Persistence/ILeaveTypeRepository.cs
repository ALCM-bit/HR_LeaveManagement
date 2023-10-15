using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeUnique(string name);
    Task<bool> LeaveTypeExists(int id);
    Task<bool> LeaveTypeUpdateExists(string name);
}