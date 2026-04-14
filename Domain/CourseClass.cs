namespace Domain;

public class CourseClass
{
    public int ClassId { get; set; }
    public string Subject { get; set; }
    public string Requirements { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public CourseClass (int classId, string subject, string requirements, string description, int capacity, DateTime startTime)
        {
        ClassId = classId;
        Subject = subject;
        Requirements = requirements;
        Description = description;
        Capacity = capacity;
        StartTime = startTime;
        EndTime = DateTime.Now;
        }
    
}