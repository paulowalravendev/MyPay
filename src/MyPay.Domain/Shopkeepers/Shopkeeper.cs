using MyPay.Domain.Abstractions;
using MyPay.Domain.Shopkeepers.Events;

namespace MyPay.Domain.Shopkeepers;

public sealed class Shopkeeper : Entity<ShopkeeperId>
{
    public Shopkeeper(ShopkeeperId userId, FullName fullName, CNPJ cnpj, Email email, Password password)
        : base(userId)
    {
        FullName = fullName;
        Cnpj = cnpj;
        Email = email;
        Password = password;
    }

#pragma warning disable CS8618
    private Shopkeeper() { }

#pragma warning restore CS8618

    public FullName FullName { get; private set; }
    public CNPJ Cnpj { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    public static Shopkeeper Create(FullName fullName, CNPJ document, Email email, Password password)
    {
        var shopkeeper = new Shopkeeper(ShopkeeperId.New(), fullName, document, email, password);

        shopkeeper.RaiseDomainEvent(new ShopkeeperCreatedDomainEvent(shopkeeper.Id));

        return shopkeeper;
    }
}
