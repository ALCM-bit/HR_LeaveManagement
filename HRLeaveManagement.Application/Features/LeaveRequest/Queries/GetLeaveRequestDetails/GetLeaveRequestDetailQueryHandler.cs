﻿using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = _mapper.Map<LeaveRequestDetailsDto>(await
            _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

        return leaveRequest;
    }
}