namespace Domain.ViewModels;

public class EnrollmentView
{
    public int EnrollmentId { get; set; }
    public string StudentName { get; set; } = "";
    public string TeacherName { get; set; } = "";
    public int RoomNumber { get; set; }
    public string Subject { get; set; } = "";
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public DateTime DateEnrolled { get; set; }
    public string Status { get; set; } = "";
}