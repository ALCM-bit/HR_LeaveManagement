using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Logging;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;

    public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IEmailSender emailSender,
        IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }
    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);
        }

        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpadateAsync(leaveRequest);
        
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request fot {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} "
                       + $"has been cchanged successfully.",
                Subject = "Leave Request Submitted"

            };
            await _emailSender.SendEmail(email);

        }
        catch (Exception e)
        {
            _appLogger.LogWarning(e.Message);
        }

        return Unit.Value;
    }
}