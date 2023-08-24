using MyPay.Application.Abstractions.Cryptography;
using MyPay.Application.Abstractions.Messaging;
using MyPay.Domain.Abstractions;
using MyPay.Domain.Shopkeepers;

namespace MyPay.Application.Features.Shopkeepers.RegisterShopkeeper;

internal sealed class RegisterShopkeeperCommandHandler : ICommandHandler<RegisterShopkeeperCommand, Guid>
{
    private readonly IShopkeeperRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterShopkeeperCommandHandler(IShopkeeperRepository customerRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterShopkeeperCommand request, CancellationToken cancellationToken)
    {
        var cnpjOrEmailAlreadyInUse = await _customerRepository.HasCnpjOrEmailAlreadyRegisterAsync(
            new CNPJ(request.Cnpj),
            new Email(request.Email),
            cancellationToken);

        if (cnpjOrEmailAlreadyInUse)
            // @TODO: Change for specif exception
            throw new InvalidOperationException();

        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher.HashPassword(request.Password, salt);

        var customer = Shopkeeper.Create(
            new FullName(request.Fullname),
            new CNPJ(request.Cnpj),
            new Email(request.Email),
            new PasswordHash(hash),
            new PasswordSalt(salt));

        _customerRepository.Add(customer);
        // @TODO: Can cnpj or email already register exception
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}