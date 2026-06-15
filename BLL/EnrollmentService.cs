using BLL.Interfaces;
using Domain.Entities;

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
            dateRequested: DateTime.Now,
            status: "Pending"
        );

        _enrollmentRepository.AddEnrollment(enrollment);
    }
}