using Domain.Entities;

namespace BLL.Interfaces;

public interface IClassroomRepository
{
    List<Classroom> GetAvailableClassrooms();
}