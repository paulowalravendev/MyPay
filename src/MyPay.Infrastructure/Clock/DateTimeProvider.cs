﻿using MyPay.Application.Abstractions.Clock;

namespace MyPay.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
