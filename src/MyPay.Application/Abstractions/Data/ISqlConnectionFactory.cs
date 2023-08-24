using System.Data;

namespace MyPay.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}