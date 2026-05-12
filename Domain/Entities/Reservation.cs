namespace Domain.Entities;

public class Reservation
{
    public int ReservationId { get; private set; }
    public int TeacherUserId { get; private set; }
    public int ClassroomId { get; private set; }
    public string Subject { get; private set; } = "";
    public DateTime ReservationTime { get; private set; }
    public DateTime DateRequested { get; private set; }
    public string Status { get; private set; } = "";

    private Reservation() { }

    public Reservation(
        int reservationId,
        int teacherUserId,
        int classroomId,
        string subject,
        DateTime reservationTime,
        DateTime dateRequested,
        string status)
    {
        ReservationId = reservationId;
        TeacherUserId = teacherUserId;
        ClassroomId = classroomId;
        Subject = subject;
        ReservationTime = reservationTime;
        DateRequested = dateRequested;
        Status = status;
    }
}