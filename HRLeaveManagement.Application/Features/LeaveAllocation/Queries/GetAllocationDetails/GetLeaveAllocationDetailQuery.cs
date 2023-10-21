using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllocationDetails;

public class GetLeaveAllocationDetailQuery : IRequest<LeaveAllocationDetailsDto>
{
    public int Id { get; set; }
}