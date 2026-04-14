namespace Domain;

public class TeacherDetails
{
    public int UserId { get; private set; }
    public string Specialization { get; private set; } = "";

    public TeacherDetails(int userId, string specialization)
    {
        UserId = userId;
        Specialization = specialization;
    }
}