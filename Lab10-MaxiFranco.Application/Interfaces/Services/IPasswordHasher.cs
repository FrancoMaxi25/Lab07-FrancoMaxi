namespace Lab10_MaxiFranco.Application.Interfaces.Services;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string password, string passwordHash);
}