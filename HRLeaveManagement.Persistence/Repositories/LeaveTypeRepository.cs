using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HRDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return await _context.LeaveTypes.AnyAsync(q => q.Name == name) == false;
    }

    public async Task<bool> LeaveTypeExists(int id)
    {
        var data = await _context.LeaveTypes.AnyAsync(q => q.Id == id);

        if (data == null) return false;

        return true;
    }

    public async Task<bool> LeaveTypeUpdateExists(string name)
    {
        var data = await _context.LeaveTypes.AnyAsync(q => q.Name == name);

        if (data == null) return false;

        return true;
    }
}