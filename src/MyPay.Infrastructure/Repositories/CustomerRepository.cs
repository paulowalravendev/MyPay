using Microsoft.EntityFrameworkCore;
using MyPay.Domain.Customers;

namespace MyPay.Infrastructure.Repositories;

internal sealed class CustomerRepository : Repository<Customer, CustomerId>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Customer?> GetByCpfAsync(CPF cpf, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Customer>().FirstOrDefaultAsync(c => c.Cpf == cpf, cancellationToken);
    }

    public async Task<bool> HasCpfOrEmailAlreadyRegisterAsync(CPF cpf, Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Customer>().AnyAsync(c => c.Email == email || c.Cpf == cpf, cancellationToken);
    }
}