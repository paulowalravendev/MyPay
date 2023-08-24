using MyPay.Domain.Abstractions;

namespace MyPay.Domain.Shopkeepers;

public static class ShopkeeperErrors
{
    public static Error NotFound = new(
        "Shopkeeper.Found",
        "The shopkeeper with the specified identifier was not found");

    public static Error InvalidCredentials = new(
        "Shopkeeper.InvalidCredentials",
        "The provided credentials were invalid");
}
