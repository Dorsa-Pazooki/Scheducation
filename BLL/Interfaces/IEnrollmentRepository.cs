using Domain.Entities;

namespace BLL.Interfaces;

public interface IEnrollmentRepository
{
    void AddEnrollment(Enrollment enrollment);
    List<Enrollment> GetAllEnrollments();
}