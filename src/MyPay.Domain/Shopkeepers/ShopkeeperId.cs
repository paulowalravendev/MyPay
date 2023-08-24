namespace MyPay.Domain.Shopkeepers;

public record ShopkeeperId(Guid Value)
{
    public static ShopkeeperId New() => new(Guid.NewGuid());
}
