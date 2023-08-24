using MyPay.Application.Abstractions.Authentication;
using MyPay.Application.Abstractions.Cryptography;
using MyPay.Application.Abstractions.Messaging;
using MyPay.Domain.Abstractions;
using MyPay.Domain.Shopkeepers;

namespace MyPay.Application.Features.Shopkeepers.LoginShopkeeper;

internal sealed class LoginShopkeeperCommandHandler : ICommandHandler<LoginShopkeeperCommand, AccessTokenResponse>
{
    private readonly IShopkeeperRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationService _autenticationService;

    public LoginShopkeeperCommandHandler(IShopkeeperRepository customerRepository, IPasswordHasher passwordHasher, IAuthenticationService autenticationService)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _autenticationService = autenticationService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LoginShopkeeperCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByCnpjAsync(new CNPJ(request.Cnpj), cancellationToken);
        if (customer is null)
            // @TODO: Change for specif exception
            throw new InvalidOperationException();

        var passwordMatch = _passwordHasher.VerifyPassword(customer.Hash.Value, customer.Salt.Value, request.Password);
        if (!passwordMatch)
            // @TODO: Change for specif exception
            throw new InvalidOperationException();

        var token = _autenticationService.GenerateJwtToken(customer);
        return new AccessTokenResponse(token);
    }
}