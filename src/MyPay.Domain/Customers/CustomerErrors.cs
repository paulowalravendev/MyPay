namespace MyPay.Domain.Customers;

using MyPay.Domain.Abstractions;

public static class CustomerErrors
{
    public static Error NotFound = new(
        "Customer.Found",
        "The customer with the specified identifier was not found");

    public static Error InvalidCredentials = new(
        "Customer.InvalidCredentials",
        "The provided credentials were invalid");

    public static Error EmailOrCpfAlreadyInUse = new(
        "Customer.EmailOrCpfAlreadyInUse",
        "Email or CPF already in use");
}
