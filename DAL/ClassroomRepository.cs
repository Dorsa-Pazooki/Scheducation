using Domain.Entities;

namespace DAL;

public class ClassroomRepository : IClassroomRepository
{
    private readonly AppDbContext _context;

    public ClassroomRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Classroom> GetAvailableClassrooms()
    {
        return _context.Classrooms.ToList();
    }
}