using BLL.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Domain.ViewModels;

namespace DAL;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly string _connectionString;

    public EnrollmentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    INSERT INTO Enrollments
                    (StudentUserId, ReservationId, DateEnrolled, Status)
                    VALUES
                    (@StudentUserId, @ReservationId, @DateEnrolled, @Status)
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@StudentUserId", enrollment.StudentUserId);
        command.Parameters.AddWithValue("@ReservationId", enrollment.ReservationId);
        command.Parameters.AddWithValue("@DateEnrolled", enrollment.DateEnrolled);
        command.Parameters.AddWithValue("@Status", enrollment.Status);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Enrollment> GetAllEnrollments()
    {
        return new List<Enrollment>();
    }
    public List<EnrollmentView> GetEnrollmentRequests()
    {
        var enrollments = new List<EnrollmentView>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
                    SELECT 
                        e.EnrollmentId,
                        s.FirstName + ' ' + s.LastName AS StudentName,
                        t.FirstName + ' ' + t.LastName AS TeacherName,
                        c.RoomNumber,
                        r.Subject,
                        r.StartDateTime,
                        r.EndDateTime,
                        e.DateEnrolled,
                        e.Status
                    FROM Enrollments e
                    INNER JOIN Users s ON e.StudentUserId = s.UserId
                    INNER JOIN Reservations r ON e.ReservationId = r.ReservationId
                    INNER JOIN Users t ON r.TeacherUserId = t.UserId
                    INNER JOIN Classrooms c ON r.ClassroomId = c.ClassroomId
                    ORDER BY e.DateEnrolled DESC
                    """;

        using var command = new SqlCommand(query, connection);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            enrollments.Add(new EnrollmentView
            {
                EnrollmentId = reader.GetInt32(0),
                StudentName = reader.GetString(1),
                TeacherName = reader.GetString(2),
                RoomNumber = reader.GetInt32(3),
                Subject = reader.GetString(4),
                StartDateTime = reader.GetDateTime(5),
                EndDateTime = reader.GetDateTime(6),
                DateEnrolled = reader.GetDateTime(7),
                Status = reader.GetString(8)
            });
        }

        return enrollments;
    }
    public void UpdateEnrollmentStatus(int enrollmentId, string status)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    UPDATE Enrollments
                    SET Status = @Status
                    WHERE EnrollmentId = @EnrollmentId
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Status", status);
        command.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

        connection.Open();
        command.ExecuteNonQuery();
    }
    public List<EnrollmentView> GetStudentEnrollments(int studentUserId, string? status)
    {
        var enrollments = new List<EnrollmentView>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
                    SELECT 
                        e.EnrollmentId,
                        s.FirstName + ' ' + s.LastName AS StudentName,
                        t.FirstName + ' ' + t.LastName AS TeacherName,
                        c.RoomNumber,
                        r.Subject,
                        r.StartDateTime,
                        r.EndDateTime,
                        e.DateEnrolled,
                        e.Status
                    FROM Enrollments e
                    INNER JOIN Users s ON e.StudentUserId = s.UserId
                    INNER JOIN Reservations r ON e.ReservationId = r.ReservationId
                    INNER JOIN Users t ON r.TeacherUserId = t.UserId
                    INNER JOIN Classrooms c ON r.ClassroomId = c.ClassroomId
                    WHERE e.StudentUserId = @StudentUserId
                    AND (@Status IS NULL OR e.Status = @Status)
                    ORDER BY r.StartDateTime
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@StudentUserId", studentUserId);
        command.Parameters.AddWithValue("@Status",
            string.IsNullOrWhiteSpace(status) ? DBNull.Value : status);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            enrollments.Add(new EnrollmentView
            {
                EnrollmentId = reader.GetInt32(0),
                StudentName = reader.GetString(1),
                TeacherName = reader.GetString(2),
                RoomNumber = reader.GetInt32(3),
                Subject = reader.GetString(4),
                StartDateTime = reader.GetDateTime(5),
                EndDateTime = reader.GetDateTime(6),
                DateEnrolled = reader.GetDateTime(7),
                Status = reader.GetString(8)
            });
        }

        return enrollments;
    }
}