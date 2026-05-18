using Domain.Entities;
using Domain.ViewModels;
using Microsoft.Data.SqlClient;

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
            (TeacherUserId, ClassroomId, Subject, ReservationTime, DateRequested, Status)
            VALUES
            (@TeacherUserId, @ClassroomId, @Subject, @ReservationTime, @DateRequested, @Status)
            """;

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@TeacherUserId", reservation.TeacherUserId);
        command.Parameters.AddWithValue("@ClassroomId", reservation.ClassroomId);
        command.Parameters.AddWithValue("@Subject", reservation.Subject);
        command.Parameters.AddWithValue("@ReservationTime", reservation.ReservationTime);
        command.Parameters.AddWithValue("@DateRequested", reservation.DateRequested);
        command.Parameters.AddWithValue("@Status", reservation.Status);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Reservation> GetAllReservations()
    {
        var reservations = new List<Reservation>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
            SELECT ReservationId, TeacherUserId, ClassroomId, Subject, ReservationTime, DateRequested, Status
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
                reservationTime: reader.GetDateTime(4),
                dateRequested: reader.GetDateTime(5),
                status: reader.GetString(6)
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
                r.ReservationTime,
                r.DateRequested,
                r.Status
            FROM Reservations r
            INNER JOIN Users u ON r.TeacherUserId = u.UserId
            INNER JOIN Classrooms c ON r.ClassroomId = c.ClassroomId
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
                ReservationTime = reader.GetDateTime(4),
                DateRequested = reader.GetDateTime(5),
                Status = reader.GetString(6)
            };

            reservationViews.Add(reservationView);
        }

        return reservationViews;
    }
}