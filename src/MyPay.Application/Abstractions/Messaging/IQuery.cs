using MyPay.Domain.Abstractions;
using MediatR;

namespace MyPay.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}