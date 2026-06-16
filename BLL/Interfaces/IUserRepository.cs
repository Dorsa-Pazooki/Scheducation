using Domain.Entities;

namespace BLL.Interfaces;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void UpdatePasswordHash(int userId, string passwordHash);
    
    string? GetUserRole(int userId);
    
    int CreateUser(string firstName, string lastName, string email, string passwordHash);
    void AddUserRole(int userId, int roleId);
}