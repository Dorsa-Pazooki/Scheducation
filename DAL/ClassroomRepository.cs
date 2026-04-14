using Domain;

namespace DAL;

public class ClassroomRepository : IClassroomRepository
{
    public List<Classroom> GetAvailableClassrooms()
    {
        return new List<Classroom>
        {
            new Classroom(1, 30, "Building A", 101),
            new Classroom(2, 25, "Building A", 102),
            new Classroom(3, 40, "Building A", 103),
            new Classroom(4, 20, "Building B", 201),
            new Classroom(5, 35, "Building B", 202),
            new Classroom(6, 50, "Building B", 203),
            new Classroom(7, 15, "Building C", 301),
            new Classroom(8, 45, "Building C", 301),
            new Classroom(9, 60, "Building C", 304),
        };
    }
}