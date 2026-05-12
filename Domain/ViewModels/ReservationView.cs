namespace Domain.ViewModels;

public class ReservationView
{
    public int ReservationId { get; set; }
    public string TeacherName { get; set; } = "";
    public int RoomNumber { get; set; }
    public string Subject { get; set; } = "";
    public DateTime ReservationTime { get; set; }
    public DateTime DateRequested { get; set; }
    public string Status { get; set; } = "";
}