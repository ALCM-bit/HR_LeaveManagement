using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllocations;

public class GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>
{
    
}