using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Logging;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CanceLeaverequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CanceLeaverequestCommandHandler> _appLogger;

    public CanceLeaverequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CanceLeaverequestCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }
    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }

        leaveRequest.Cancelled = true;
        
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request fot {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} "
                       + $"has been cancelled successfully.",
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