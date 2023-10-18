using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllocations;

public class GetLeaveAllocationListHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public GetLeaveAllocationListHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository
            .GetLeaveAllocationsWithDetails();
        var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocation);

        return allocations;
    }
}