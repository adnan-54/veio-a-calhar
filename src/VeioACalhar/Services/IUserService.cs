using VeioACalhar.Models;

namespace VeioACalhar.Services;

public interface IUserService
{
    Usuario CurrentUser { get; }

    Usuario SignUp(string login, string password);

    void SignIn(string login, string password);

    void SignOut();
}
