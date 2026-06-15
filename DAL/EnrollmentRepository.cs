using BLL.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;

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
                    (StudentUserId, ReservationId, DateRequested, Status)
                    VALUES
                    (@StudentUserId, @ReservationId, @DateRequested, @Status)
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@StudentUserId", enrollment.StudentUserId);
        command.Parameters.AddWithValue("@ReservationId", enrollment.ReservationId);
        command.Parameters.AddWithValue("@DateRequested", enrollment.DateRequested);
        command.Parameters.AddWithValue("@Status", enrollment.Status);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Enrollment> GetAllEnrollments()
    {
        return new List<Enrollment>();
    }
}