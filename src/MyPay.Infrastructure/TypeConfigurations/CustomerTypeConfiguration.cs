using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyPay.Domain.Customers;

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
            .HasConversion(FullName => FullName.Value, value => new FullName(value));

        builder.Property(customer => customer.Cpf)
            .HasMaxLength(11)
            .HasConversion(cpf => cpf.Value, value => new CPF(value));

        builder.Property(customer => customer.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(customer => customer.Hash)
            .HasMaxLength(400)
            .HasConversion(hash => hash.Value, value => new PasswordHash(value));

        builder.Property(customer => customer.Salt)
            .HasMaxLength(400)
            .HasConversion(salt => salt.Value, value => new PasswordSalt(value));

        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.Cpf).IsUnique();
    }
}
