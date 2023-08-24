using MyPay.Domain.Abstractions;
using MyPay.Domain.Customers.Events;

namespace MyPay.Domain.Customers;

public sealed class Customer : Entity<CustomerId>
{
    public Customer(CustomerId userId, FullName fullName, CPF cpf, Email email, PasswordHash hash, PasswordSalt salt)
        : base(userId)
    {
        FullName = fullName;
        Cpf = cpf;
        Email = email;
        Hash = hash;
        Salt = salt;
    }


#pragma warning disable CS8618
    private Customer() { }
#pragma warning restore CS8618

    public FullName FullName { get; private set; }
    public CPF Cpf { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash Hash { get; private set; }
    public PasswordSalt Salt { get; private set; }

    public static Customer Create(FullName fullName, CPF document, Email email, PasswordHash hash, PasswordSalt salt)
    {
        var customer = new Customer(CustomerId.New(), fullName, document, email, hash, salt);
        customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(customer.Id));
        return customer;
    }
}
