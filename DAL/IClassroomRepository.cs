using Domain;

namespace DAL;

public interface IClassroomRepository
{
    List<Classroom> GetAvailableClassrooms();
}