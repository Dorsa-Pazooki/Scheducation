using BLL.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DAL;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public User? GetUserByEmail(string email)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    SELECT UserId, FirstName, LastName, Email, PasswordHash
                    FROM Users
                    WHERE Email = @Email
                    """;

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Email", email);

        connection.Open();

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User(
                userId: reader.GetInt32(0),
                firstName: reader.GetString(1),
                lastName: reader.GetString(2),
                email: reader.GetString(3),
                passwordHash: reader.IsDBNull(4) ? "" : reader.GetString(4)
            );
        }

        return null;
    }

    public void UpdatePasswordHash(int userId, string passwordHash)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    UPDATE Users
                    SET PasswordHash = @PasswordHash
                    WHERE UserId = @UserId
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
        command.Parameters.AddWithValue("@UserId", userId);

        connection.Open();
        command.ExecuteNonQuery();
    }
    
    public string? GetUserRole(int userId)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    SELECT r.RoleName
                    FROM UserRoles ur
                    INNER JOIN Roles r ON ur.RoleId = r.RoleId
                    WHERE ur.UserId = @UserId
                    """;

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", userId);

        connection.Open();

        var result = command.ExecuteScalar();

        return result?.ToString();
    }
    
    public int CreateUser(string firstName, string lastName, string email, string passwordHash)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    INSERT INTO Users
                    (FirstName, LastName, Email, PasswordHash)
                    OUTPUT INSERTED.UserId
                    VALUES
                    (@FirstName, @LastName, @Email, @PasswordHash)
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@FirstName", firstName);
        command.Parameters.AddWithValue("@LastName", lastName);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);

        connection.Open();

        return (int)command.ExecuteScalar();
    }
    
    public void AddUserRole(int userId, int roleId)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    INSERT INTO UserRoles
                    (UserId, RoleId)
                    VALUES
                    (@UserId, @RoleId)
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserId", userId);
        command.Parameters.AddWithValue("@RoleId", roleId);

        connection.Open();
        command.ExecuteNonQuery();
    }
}