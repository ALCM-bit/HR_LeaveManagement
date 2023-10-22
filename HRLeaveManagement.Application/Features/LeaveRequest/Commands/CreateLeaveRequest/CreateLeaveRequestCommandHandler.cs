using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Logging;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

    public CreateLeaveRequestCommandHandler(IEmailSender emailSender,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _appLogger = appLogger;
    }
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request);

        if (validatorResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Request", validatorResult);
        }

        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        await _leaveRequestRepository.CreateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request fot {request.StartDate:D} to {request.EndDate:D} "
                       + $"has been created successfully.",
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