using BLL;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class StudentModel : PageModel
{
    private readonly ReservationService _reservationService;
    private readonly EnrollmentService _enrollmentService;

    public List<ReservationView> ApprovedReservations { get; set; } = new();
    public string Message { get; set; } = "";

    public StudentModel(
        ReservationService reservationService,
        EnrollmentService enrollmentService)
    {
        _reservationService = reservationService;
        _enrollmentService = enrollmentService;
    }

    public void OnGet()
    {
        ApprovedReservations = _reservationService.GetApprovedReservations();
    }

    public void OnPostEnroll(int reservationId)
    {
        _enrollmentService.EnrollStudent(
            studentUserId: 2,
            reservationId: reservationId
        );

        Message = "Enrollment request sent.";
        ApprovedReservations = _reservationService.GetApprovedReservations();
    }
}