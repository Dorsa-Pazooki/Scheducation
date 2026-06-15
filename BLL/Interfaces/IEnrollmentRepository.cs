using Domain.Entities;
using Domain.ViewModels;

namespace BLL.Interfaces;

public interface IEnrollmentRepository
{
    void AddEnrollment(Enrollment enrollment);
    List<Enrollment> GetAllEnrollments();

    List<EnrollmentView> GetEnrollmentRequests();
    List<EnrollmentView> GetStudentEnrollments(int studentUserId, string? status);
    void UpdateEnrollmentStatus(int enrollmentId, string status);
}