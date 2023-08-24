using MyPay.Application.Abstractions.Authentication;
using MyPay.Application.Abstractions.Cryptography;
using MyPay.Application.Abstractions.Messaging;
using MyPay.Domain.Abstractions;
using MyPay.Domain.Customers;

namespace MyPay.Application.Features.Customers.LoginCustomer;

internal sealed class LoginCustomerCommandHandler : ICommandHandler<LoginCustomerCommand, AccessTokenResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationService _autenticationService;

    public LoginCustomerCommandHandler(ICustomerRepository customerRepository, IPasswordHasher passwordHasher, IAuthenticationService autenticationService)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _autenticationService = autenticationService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByCpfAsync(new CPF(request.Cpf), cancellationToken);
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