namespace MyPay.Application.Abstractions.Authentication;

public interface IUserContext
{
    string UserId { get; }
    string Type { get; }
}