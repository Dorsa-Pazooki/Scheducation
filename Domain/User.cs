namespace Domain;

public class User
{
    public int UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public List<string> Roles { get; set; }
    
    public User(int userId, string firstName, string lastName, string email)
        {
        UserID = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        }
    
    public void AddRole(string role)
    {
        if (!Roles.Contains(role))
        {
            Roles.Add(role);
        }
    }
    
}