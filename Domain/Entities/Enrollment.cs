namespace Domain.Entities;

public class Enrollment
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Status { get; set; }
    
    public Enrollment (int enrollmentId, int studentId, int classId, DateTime enrollmentDate, string status)
        {
        EnrollmentId = enrollmentId;
        StudentId = studentId;
        ClassId = classId;
        EnrollmentDate = enrollmentDate;
        Status = status;
        }
}