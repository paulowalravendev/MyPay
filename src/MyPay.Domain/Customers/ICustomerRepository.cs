namespace MyPay.Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByCpfAsync(CPF cpf, CancellationToken cancellationToken = default);
    Task<bool> HasCpfOrEmailAlreadyRegisterAsync(CPF cpf, Email email, CancellationToken cancellationToken = default);
    void Add(Customer customer);
}