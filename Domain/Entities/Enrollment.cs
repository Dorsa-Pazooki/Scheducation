namespace Domain.Entities;

public class Enrollment
{
    public int EnrollmentId { get; private set; }
    public int StudentUserId { get; private set; }
    public int ReservationId { get; private set; }
    public DateTime DateRequested { get; private set; }
    public string Status { get; private set; }

    public Enrollment(
        int enrollmentId,
        int studentUserId,
        int reservationId,
        DateTime dateRequested,
        string status)
    {
        EnrollmentId = enrollmentId;
        StudentUserId = studentUserId;
        ReservationId = reservationId;
        DateRequested = dateRequested;
        Status = status;
    }
}