using BLL.Interfaces;
using Domain.Entities;
using Domain.ViewModels;

namespace BLL;

public class EnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public void EnrollStudent(int studentUserId, int reservationId)
    {
        var enrollment = new Enrollment(
            enrollmentId: 0,
            studentUserId: studentUserId,
            reservationId: reservationId,
            dateEnrolled: DateTime.Now,
            status: "Pending"
        );

        _enrollmentRepository.AddEnrollment(enrollment);
    }

    public List<EnrollmentView> GetEnrollmentRequests()
    {
        return _enrollmentRepository.GetEnrollmentRequests();
    }

    public List<EnrollmentView> GetStudentEnrollments(int studentUserId, string? status)
    {
        return _enrollmentRepository.GetStudentEnrollments(studentUserId, status);
    }

    public void ApproveEnrollment(int enrollmentId)
    {
        _enrollmentRepository.UpdateEnrollmentStatus(enrollmentId, "Approved");
    }

    public void RejectEnrollment(int enrollmentId)
    {
        _enrollmentRepository.UpdateEnrollmentStatus(enrollmentId, "Rejected");
    }
}