using MyPay.Domain.Abstractions;
using MyPay.Domain.Customers;
using MyPay.Domain.Shopkeepers.Events;

namespace MyPay.Domain.Shopkeepers;

public sealed class Shopkeeper : Entity<ShopkeeperId>
{
    public Shopkeeper(ShopkeeperId userId, FullName fullName, CNPJ cnpj, Email email, PasswordHash hash, PasswordSalt salt)
        : base(userId)
    {
        FullName = fullName;
        Cnpj = cnpj;
        Email = email;
        Hash = hash;
        Salt = salt;
    }

#pragma warning disable CS8618
    private Shopkeeper() { }

#pragma warning restore CS8618

    public FullName FullName { get; private set; }
    public CNPJ Cnpj { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash Hash { get; private set; }
    public PasswordSalt Salt { get; private set; }

    public static Shopkeeper Create(FullName fullName, CNPJ document, Email email, PasswordHash hash, PasswordSalt salt)
    {
        var shopkeeper = new Shopkeeper(ShopkeeperId.New(), fullName, document, email, hash, salt);

        shopkeeper.RaiseDomainEvent(new ShopkeeperCreatedDomainEvent(shopkeeper.Id));

        return shopkeeper;
    }
}
