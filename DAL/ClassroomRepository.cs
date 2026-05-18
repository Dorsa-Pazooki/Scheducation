using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DAL;

public class ClassroomRepository : IClassroomRepository
{
    private readonly string _connectionString;

    public ClassroomRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Classroom> GetAvailableClassrooms()
    {
        var classrooms = new List<Classroom>();

        using var connection = new SqlConnection(_connectionString);

        var query = """
                    SELECT ClassroomId, Capacity, Location, RoomNumber
                    FROM Classrooms
                    """;

        using var command = new SqlCommand(query, connection);

        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var classroom = new Classroom(
                classroomId: reader.GetInt32(0),
                capacity: reader.GetInt32(1),
                location: reader.GetString(2),
                roomNumber: reader.GetInt32(3)
            );

            classrooms.Add(classroom);
        }

        return classrooms;
    }
}