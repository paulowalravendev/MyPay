namespace MyPay.Domain.Shopkeepers;

public interface IShopkeeperRepository
{
    Task<Shopkeeper?> GetByCnpjAsync(CNPJ cnpj, CancellationToken cancellationToken = default);
    Task<bool> HasCnpjOrEmailAlreadyRegisterAsync(CNPJ cnpj, Email email, CancellationToken cancellationToken = default);
    void Add(Shopkeeper customer);
}