using MyPay.Application.Abstractions.Cryptography;
using System.Security.Cryptography;

namespace MyPay.Infrastructure.Cryptography;

internal sealed class PasswordHasher : IPasswordHasher
{
    public string GenerateSalt()
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
        var hashBytes = pbkdf2.GetBytes(32);
        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string storedHash, string storedSalt, string providedPassword)
    {
        string providedHash = HashPassword(providedPassword, storedSalt);
        return storedHash == providedHash;
    }
}
