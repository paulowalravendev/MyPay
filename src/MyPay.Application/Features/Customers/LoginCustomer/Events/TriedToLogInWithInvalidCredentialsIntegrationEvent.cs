using MyPay.Application.Abstractions.IntegrationEvents;
using MyPay.Domain.Customers;

namespace MyPay.Application.Features.Customers.LoginCustomer.Events;

public sealed record TriedToLogInWithInvalidCredentialsIntegrationEvent(CPF Cpf) : IIntegrationEvent;
