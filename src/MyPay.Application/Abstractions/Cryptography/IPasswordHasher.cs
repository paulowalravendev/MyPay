namespace MyPay.Application.Abstractions.Cryptography;

public interface IPasswordHasher
{
    string GenerateSalt();
    string HashPassword(string password, string salt);
    bool VerifyPassword(string storedHash, string storedSalt, string providedPassword);
}
