using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyPay.Domain.Customers;
using MyPay.Domain.Shopkeepers;

namespace MyPay.Infrastructure.TypeConfigurations;

internal sealed class ShopkeeperTypeConfiguration : IEntityTypeConfiguration<Shopkeeper>
{
    public void Configure(EntityTypeBuilder<Shopkeeper> builder)
    {
        builder.ToTable("shopkeepers");

        builder.HasKey(shopkeeper => shopkeeper.Id);

        builder.Property(shopkeeper => shopkeeper.Id)
            .HasConversion(shopkeeperId => shopkeeperId.Value, value => new ShopkeeperId(value));

        builder.Property(shopkeeper => shopkeeper.FullName)
            .HasMaxLength(200)
            .HasConversion(FullName => FullName.Value, value => new Domain.Shopkeepers.FullName(value));

        builder.Property(shopkeeper => shopkeeper.Cnpj)
            .HasMaxLength(14)
            .HasConversion(cnpj => cnpj.Value, value => new CNPJ(value));

        builder.Property(shopkeeper => shopkeeper.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Domain.Shopkeepers.Email(value));

        builder.Property(shopkeeper => shopkeeper.Hash)
            .HasMaxLength(400)
            .HasConversion(hash => hash.Value, value => new Domain.Shopkeepers.PasswordHash(value));

        builder.Property(shopkeeper => shopkeeper.Salt)
            .HasMaxLength(400)
            .HasConversion(salt => salt.Value, value => new Domain.Shopkeepers.PasswordSalt(value));

        builder.HasIndex(shopkeeper => shopkeeper.Email).IsUnique();
        builder.HasIndex(shopkeeper => shopkeeper.Cnpj).IsUnique();
    }
}
