using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandValidator : AbstractValidator<DeleteLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;


    public DeleteLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyId} is required")
            .NotNull().WithMessage("{PropertyId} cannot be null");

        RuleFor(q => q)
            .MustAsync(LeaveTypeExists)
            .WithMessage("Leave type not exist");
    }

    private Task<bool> LeaveTypeExists(DeleteLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.LeaveTypeExists(command.Id);
    }
}