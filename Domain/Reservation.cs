namespace Domain;

public class Reservation
{
    public int ReservationId { get; private set; }
    public int TeacherId { get; private set; }
    public int ClassroomId { get; private set; }
    public string Subject { get; private set; }
    public DateTime ReservationTime { get; private set; }
    public string Status { get; private set; }

    public Reservation(
        int reservationId,
        int teacherId,
        int classroomId,
        string subject,
        DateTime reservationTime,
        string status)
    {
        ReservationId = reservationId;
        TeacherId = teacherId;
        ClassroomId = classroomId;
        Subject = subject;
        ReservationTime = reservationTime;
        Status = status;
    }
}