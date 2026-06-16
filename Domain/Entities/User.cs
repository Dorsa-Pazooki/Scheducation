namespace Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public List<string> Roles { get; set; }
    
    public string PasswordHash { get; }
    
    public User(int userId, string firstName, string lastName, string email, string passwordHash)
        {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        }
    
    public void AddRole(string role)
    {
        if (!Roles.Contains(role))
        {
            Roles.Add(role);
        }
    }
    
}