using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPay.Application.Abstractions.Authentication;
using MyPay.Application.Features.Customers.LoginCustomer;
using MyPay.Application.Features.Customers.RegisterCustomer;
using MyPay.Infrastructure.Policies;

namespace MyPay.Api.Controllers.Customers;

[Authorize]
[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContext _userContext;

    public CustomersController(ISender sender, IUserContext userContext)
    {
        _sender = sender;
        _userContext = userContext;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCustomerCommand(
            request.FullName,
            request.Email,
            request.Cpf,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCustomerCommand(
            request.Cpf,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize(Policy = IdentityData.CustomerPolicy)]
    [HttpGet("whoiam")]
    public IActionResult WhoIAm()
    {
        return Ok(new { _userContext.UserId, _userContext.Type });
    }
}
