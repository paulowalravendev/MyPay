using MyPay.Application.Abstractions.Cryptography;
using MyPay.Application.Abstractions.Messaging;
using MyPay.Domain.Abstractions;
using MyPay.Domain.Customers;

namespace MyPay.Application.Features.Customers.RegisterCustomer;

internal sealed class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var cpfOrEmailAlreadyInUse = await _customerRepository.HasCpfOrEmailAlreadyRegisterAsync(
            new CPF(request.Cpf),
            new Email(request.Email),
            cancellationToken);

        if (cpfOrEmailAlreadyInUse)
            // @TODO: Change for specif exception
            throw new InvalidOperationException();

        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher.HashPassword(request.Password, salt);

        var customer = Customer.Create(
            new FullName(request.Fullname),
            new CPF(request.Cpf),
            new Email(request.Email),
            new PasswordHash(hash),
            new PasswordSalt(salt));

        _customerRepository.Add(customer);
        // @TODO: Can cpf or email already register exception
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}