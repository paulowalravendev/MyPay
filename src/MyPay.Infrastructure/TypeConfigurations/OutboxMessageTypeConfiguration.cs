﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyPay.Infrastructure.Outbox;

namespace MyPay.Infrastructure.TypeConfigurations;

internal sealed class OutboxMessageTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        builder.HasKey(outboxMessage => outboxMessage.Id);
        builder.Property(outboxMessage => outboxMessage.Content).HasColumnType("json");
    }
}
