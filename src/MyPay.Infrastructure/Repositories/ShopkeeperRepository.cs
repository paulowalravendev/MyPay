using Microsoft.EntityFrameworkCore;
using MyPay.Domain.Shopkeepers;

namespace MyPay.Infrastructure.Repositories;

internal sealed class ShopkeeperRepository : Repository<Shopkeeper, ShopkeeperId>, IShopkeeperRepository
{
    public ShopkeeperRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Shopkeeper?> GetByCnpjAsync(CNPJ cnpj, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Shopkeeper>().FirstOrDefaultAsync(c => c.Cnpj == cnpj, cancellationToken);
    }

    public async Task<bool> HasCnpjOrEmailAlreadyRegisterAsync(CNPJ cnpj, Domain.Shopkeepers.Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Shopkeeper>().AnyAsync(c => c.Email == email || c.Cnpj == cnpj, cancellationToken);
    }
}