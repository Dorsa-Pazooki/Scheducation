namespace Domain;

public class Schedule
{
    public int ScheduleId { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status  { get; set; }
    
    
    public Schedule (int scheduleId, DateTime date, DateTime startTime, DateTime endTime, string status)
        {
        ScheduleId = scheduleId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
        }
    
}