namespace Domain.Entities;

public class Classroom
{
    public int ClassroomId { get; private set; }
    public int Capacity { get; private set; }
    public string Location { get; private set; }
    public int RoomNumber { get; private set; }

    public Classroom(int classroomId, int capacity, string location, int roomNumber)
    {
        ClassroomId = classroomId;
        Capacity = capacity;
        Location = location;
        RoomNumber = roomNumber;
    }
}