using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Logging;
using HRLeaveManagement.Application.Models.Email;
using MediatR;
using Microsoft.VisualBasic;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IEmailSender emailSender,
        IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository,
            _leaveRequestRepository);

        var validatorResult = await validator.ValidateAsync(request);

        if (validatorResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Request", validatorResult);
        }

        _mapper.Map(request, leaveRequest);

        await _leaveRequestRepository.UpadateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request fot {request.StartDate:D} to {request.EndDate:D} "
                       + $"has been updated successfully.",
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