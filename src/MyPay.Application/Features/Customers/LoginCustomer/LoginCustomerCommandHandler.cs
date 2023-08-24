using MediatR;
using MyPay.Application.Abstractions.Authentication;
using MyPay.Application.Abstractions.Cryptography;
using MyPay.Application.Abstractions.Messaging;
using MyPay.Application.Features.Customers.LoginCustomer.Events;
using MyPay.Domain.Abstractions;
using MyPay.Domain.Customers;

namespace MyPay.Application.Features.Customers.LoginCustomer;

internal sealed class LoginCustomerCommandHandler : ICommandHandler<LoginCustomerCommand, AccessTokenResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationService _autenticationService;
    private readonly IPublisher _publisher;

    public LoginCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IPasswordHasher passwordHasher,
        IAuthenticationService autenticationService,
        IPublisher publisher)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _autenticationService = autenticationService;
        _publisher = publisher;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByCpfAsync(new CPF(request.Cpf), cancellationToken);
        if (customer is null)
        {
            await _publisher.Publish(new TriedToLogInWithInvalidCredentialsIntegrationEvent(new CPF(request.Cpf)), cancellationToken);
            return Result.Failure<AccessTokenResponse>(CustomerErrors.InvalidCredentials);
        }

        var passwordMatch = _passwordHasher.VerifyPassword(customer.Hash.Value, customer.Salt.Value, request.Password);
        if (!passwordMatch)
        {
            await _publisher.Publish(new TriedToLogInWithInvalidCredentialsIntegrationEvent(new CPF(request.Cpf)), cancellationToken);
            return Result.Failure<AccessTokenResponse>(CustomerErrors.InvalidCredentials);
        }

        var token = _autenticationService.GenerateJwtToken(customer);
        return new AccessTokenResponse(token);
    }
}