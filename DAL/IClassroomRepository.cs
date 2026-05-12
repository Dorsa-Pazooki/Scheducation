using Domain.Entities;

namespace DAL;

public interface IClassroomRepository
{
    List<Classroom> GetAvailableClassrooms();
}