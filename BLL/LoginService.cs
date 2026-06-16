using BLL.Interfaces;
using Domain.Entities;

namespace BLL;

public class LoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? Login(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            return null;
        }

        var passwordIsCorrect = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        if (!passwordIsCorrect)
        {
            return null;
        }

        return user;
    }

    public void SetPassword(int userId, string plainPassword)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);

        _userRepository.UpdatePasswordHash(userId, passwordHash);
    }
    
    public string? GetUserRole(int userId)
    {
        return _userRepository.GetUserRole(userId);
    }
    
    public void RegisterStudent(string firstName, string lastName, string email, string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var userId = _userRepository.CreateUser(firstName, lastName, email, passwordHash);

        _userRepository.AddUserRole(userId, 2);
    }
}