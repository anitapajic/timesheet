using System.Security.Cryptography;
using System.Text;

namespace TimesheetApp.Application.Common.Utils;

public static class PasswordHasher
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash =  Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            _hashAlgorithm,
            KeySize);
        return Convert.ToHexString(hash);
    }
}