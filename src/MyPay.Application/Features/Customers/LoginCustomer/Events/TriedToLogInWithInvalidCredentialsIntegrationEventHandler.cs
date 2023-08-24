using MediatR;
using Microsoft.Extensions.Logging;
using MyPay.Domain.Customers;

namespace MyPay.Application.Features.Customers.LoginCustomer.Events;

internal sealed class TriedToLogInWithInvalidCredentialsIntegrationEventHandler : INotificationHandler<TriedToLogInWithInvalidCredentialsIntegrationEvent>
{
    private readonly ILogger<TriedToLogInWithInvalidCredentialsIntegrationEventHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public TriedToLogInWithInvalidCredentialsIntegrationEventHandler(ILogger<TriedToLogInWithInvalidCredentialsIntegrationEventHandler> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task Handle(TriedToLogInWithInvalidCredentialsIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByCpfAsync(notification.Cpf, cancellationToken);
        _logger.LogWarning("CustomerId {CustomerId} try to login with invalid credentials", customer!.Id.Value);
    }
}
