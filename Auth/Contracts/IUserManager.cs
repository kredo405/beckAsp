namespace Auth.Contracts;

public interface IUserManager
{
    Task RegisterAsync(string username, string password);
    Task<string> LoginAsync(string username, string password);
}