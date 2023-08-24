using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyPay.Domain.Customers;
using MyPay.Domain.Shopkeepers;

namespace MyPay.Infrastructure.TypeConfigurations;

internal sealed class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
            .HasConversion(customerId => customerId.Value, value => new CustomerId(value));

        builder.Property(customer => customer.FullName)
            .HasMaxLength(200)
            .HasConversion(FullName => FullName.Value, value => new Domain.Customers.FullName(value));

        builder.Property(customer => customer.Cpf)
            .HasMaxLength(11)
            .HasConversion(cpf => cpf.Value, value => new CPF(value));

        builder.Property(customer => customer.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Domain.Customers.Email(value));

        builder.Property(customer => customer.Hash)
            .HasMaxLength(400)
            .HasConversion(hash => hash.Value, value => new Domain.Customers.PasswordHash(value));

        builder.Property(customer => customer.Salt)
            .HasMaxLength(400)
            .HasConversion(salt => salt.Value, value => new Domain.Customers.PasswordSalt(value));

        builder.HasIndex(customer => customer.Email).IsUnique();
        builder.HasIndex(customer => customer.Cpf).IsUnique();
    }
}
