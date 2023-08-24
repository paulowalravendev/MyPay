using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPay.Application.Abstractions.Authentication;
using MyPay.Application.Features.Shopkeepers.LoginShopkeeper;
using MyPay.Application.Features.Shopkeepers.RegisterShopkeeper;
using MyPay.Infrastructure.Policies;

namespace MyPay.Api.Controllers.Shopkeepers;

[Authorize]
[ApiController]
[Route("api/shopkeepers")]
public class ShopkeepersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContext _userContext;

    public ShopkeepersController(ISender sender, IUserContext userContext)
    {
        _sender = sender;
        _userContext = userContext;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterShopkeeperRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterShopkeeperCommand(
            request.FullName,
            request.Email,
            request.Cnpj,
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
        LoginShopkeeperRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginShopkeeperCommand(
            request.Cnpj,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize(Policy = IdentityData.ShopkeeperPolicy)]
    [HttpGet("whoiam")]
    public IActionResult WhoIAm()
    {
        return Ok(new { _userContext.UserId, _userContext.Type });
    }
}
