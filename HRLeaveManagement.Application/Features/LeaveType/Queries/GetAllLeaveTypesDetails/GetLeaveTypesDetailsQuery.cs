using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypesDetails;

public record GetLeaveTypesDatailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;