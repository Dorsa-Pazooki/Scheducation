using Domain.Entities;
using Domain.ViewModels;
using Microsoft.Data.SqlClient;
using BLL.Interfaces;


namespace DAL;

public class ReservationRepository : IReservationRepository
{
    private readonly string _connectionString;

    public ReservationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddReservation(Reservation reservation)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
            INSERT INTO Reservations
            (TeacherUserId, ClassroomId, Subject, StartDateTime, EndDateTime, DateRequested, Status)
            VALUES
            (@TeacherUserId, @ClassroomId, @Subject, @StartDateTime, @EndDateTime, @DateRequested, @Status)
            """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@TeacherUserId", reservation.TeacherUserId);
        command.Parameters.AddWithValue("@ClassroomId", reservation.ClassroomId);
        command.Parameters.AddWithValue("@Subject", reservation.Subject);
        command.Parameters.AddWithValue("@StartDateTime", reservation.StartDateTime);
        command.Parameters.AddWithValue("@EndDateTime", reservation.EndDateTime);
        command.Parameters.AddWithValue("@DateRequested", reservation.DateRequested);
        command.Parameters.AddWithValue("@Status", reservation.Status);

        Console.WriteLine("AddReservation reached");

        connection.Open();

        var rowsAffected = command.ExecuteNonQuery();

        Console.WriteLine($"Rows inserted: {rowsAffected}");
    }

    public List<Reservation> GetAllReservations()
    {
        var reservations = new List<Reservation>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
            SELECT ReservationId, TeacherUserId, ClassroomId, Subject, StartDateTime, EndDateTime, DateRequested, Status
            FROM Reservations
            """;

        using var command = new SqlCommand(query, connection);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var reservation = new Reservation(
                reservationId: reader.GetInt32(0),
                teacherUserId: reader.GetInt32(1),
                classroomId: reader.GetInt32(2),
                subject: reader.GetString(3),
                startDateTime: reader.GetDateTime(4),
                endDateTime: reader.GetDateTime(5),
                dateRequested: reader.GetDateTime(6),
                status: reader.GetString(7)
            );

            reservations.Add(reservation);
        }

        return reservations;
    }

    public List<ReservationView> GetReservationViews()
    {
        var reservationViews = new List<ReservationView>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
            SELECT 
                r.ReservationId,
                u.FirstName + ' ' + u.LastName AS TeacherName,
                c.RoomNumber,
                r.Subject,
                r.StartDateTime,
                r.EndDateTime,
                r.DateRequested,
                r.Status
            FROM Reservations r
            INNER JOIN Users u ON r.TeacherUserId = u.UserId
            INNER JOIN Classrooms c ON r.ClassroomId = c.ClassroomId
            ORDER BY r.DateRequested DESC
            """;

        using var command = new SqlCommand(query, connection);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var reservationView = new ReservationView
            {
                ReservationId = reader.GetInt32(0),
                TeacherName = reader.GetString(1),
                RoomNumber = reader.GetInt32(2),
                Subject = reader.GetString(3),
                StartDateTime = reader.GetDateTime(4),
                EndDateTime = reader.GetDateTime(5),
                DateRequested = reader.GetDateTime(6),
                Status = reader.GetString(7)
            };

            reservationViews.Add(reservationView);
        }

        return reservationViews;
    }
    public void UpdateReservationStatus(int reservationId, string status)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = """
                    UPDATE Reservations
                    SET Status = @Status
                    WHERE ReservationId = @ReservationId
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Status", status);
        command.Parameters.AddWithValue("@ReservationId", reservationId);

        connection.Open();
        command.ExecuteNonQuery();
    }
    public List<ReservationView> GetTeacherReservations(int teacherUserId, string? status)
    {
        var reservations = new List<ReservationView>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
                    SELECT 
                        r.ReservationId,
                        u.FirstName + ' ' + u.LastName AS TeacherName,
                        c.RoomNumber,
                        r.Subject,
                        r.StartDateTime,
                        r.EndDateTime,
                        r.DateRequested,
                        r.Status
                    FROM Reservations r
                    INNER JOIN Users u ON r.TeacherUserId = u.UserId
                    INNER JOIN Classrooms c ON r.ClassroomId = c.ClassroomId
                    WHERE r.TeacherUserId = @TeacherUserId
                    AND (@Status IS NULL OR r.Status = @Status)
                    ORDER BY r.StartDateTime
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@TeacherUserId", teacherUserId);
        command.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(status) ? DBNull.Value : status);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            reservations.Add(new ReservationView
            {
                ReservationId = reader.GetInt32(0),
                TeacherName = reader.GetString(1),
                RoomNumber = reader.GetInt32(2),
                Subject = reader.GetString(3),
                StartDateTime = reader.GetDateTime(4),
                EndDateTime = reader.GetDateTime(5),
                DateRequested = reader.GetDateTime(6),
                Status = reader.GetString(7)
            });
        }

        return reservations;
    }
    public List<ReservationView> GetAllReservationsByStatus(string? status)
    {
        var reservations = new List<ReservationView>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
                    SELECT 
                        r.ReservationId,
                        u.FirstName + ' ' + u.LastName AS TeacherName,
                        c.RoomNumber,
                        r.Subject,
                        r.StartDateTime,
                        r.EndDateTime,
                        r.DateRequested,
                        r.Status
                    FROM Reservations r
                    INNER JOIN Users u ON r.TeacherUserId = u.UserId
                    INNER JOIN Classrooms c ON r.ClassroomId = c.ClassroomId
                    WHERE (@Status IS NULL OR r.Status = @Status)
                    ORDER BY r.StartDateTime
                    """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(status) ? DBNull.Value : status);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            reservations.Add(new ReservationView
            {
                ReservationId = reader.GetInt32(0),
                TeacherName = reader.GetString(1),
                RoomNumber = reader.GetInt32(2),
                Subject = reader.GetString(3),
                StartDateTime = reader.GetDateTime(4),
                EndDateTime = reader.GetDateTime(5),
                DateRequested = reader.GetDateTime(6),
                Status = reader.GetString(7)
            });
        }

        return reservations;
    }
}