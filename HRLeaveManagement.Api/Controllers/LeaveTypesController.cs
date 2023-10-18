using System.Runtime.InteropServices;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypesDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<List<LeaveTypeDto>> Get()
    {
        var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return leaveTypes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
    {
        var leaveType = await _mediator.Send(new GetLeaveTypesDatailsQuery(id));
        return Ok(leaveType);
    }

    [HttpPost]
    [ProducesResponseTypeAttribute(201)]
    [ProducesResponseTypeAttribute(400)]
    public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveType)
    {
        var response = await _mediator.Send(leaveType);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseTypeAttribute(StatusCodes.Status204NoContent)]
    [ProducesResponseTypeAttribute(400)]
    [ProducesResponseTypeAttribute(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveTypeCommand leaveType)
    {
        await _mediator.Send(leaveType);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseTypeAttribute(StatusCodes.Status204NoContent)]
    [ProducesResponseTypeAttribute(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveTypeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
    
}