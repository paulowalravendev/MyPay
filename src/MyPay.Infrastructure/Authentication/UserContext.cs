using Microsoft.AspNetCore.Http;
using MyPay.Application.Abstractions.Authentication;
using MyPay.Infrastructure.Policies;

namespace MyPay.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string UserId => _contextAccessor.HttpContext?
        .User.Claims.SingleOrDefault(c => c.Type == IdentityData.UserIdName)?.Value ??
        throw new ApplicationException("User context is unavailable");

    public string Type => _contextAccessor.HttpContext?
        .User.Claims.SingleOrDefault(c => c.Type == IdentityData.CustomerPolicyKey)?.Value ??
        throw new ApplicationException("User context is unavailable");
}
