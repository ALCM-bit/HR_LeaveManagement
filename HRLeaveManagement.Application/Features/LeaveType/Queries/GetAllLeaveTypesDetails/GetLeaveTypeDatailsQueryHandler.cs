using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypesDetails;

public class GetLeaveTypeDatailsQueryHandler : IRequestHandler<GetLeaveTypesDatailsQuery, LeaveTypeDetailsDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeDatailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypesDatailsQuery request,
        CancellationToken cancellationToken)
    {
        var leaveTypes = await _leaveTypeRepository.GetByIdAsync(request.Id);

        if (leaveTypes == null) throw new NotFoundException(nameof(Domain.LeaveType), request.Id);

        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveTypes);

        return data;
    }
}