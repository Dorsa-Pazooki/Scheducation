using BLL;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class AdminModel : PageModel
{
    private readonly ReservationService _reservationService;
    private readonly ReservationApprovalService _approvalService;
    private readonly EnrollmentService _enrollmentService;

    public List<EnrollmentView> EnrollmentRequests { get; set; } = new();

    public List<ReservationView> Reservations { get; set; } = new();

    public AdminModel(
        ReservationService reservationService,
        ReservationApprovalService approvalService,
        EnrollmentService enrollmentService)
    {
        _reservationService = reservationService;
        _approvalService = approvalService;
        _enrollmentService = enrollmentService;
    }

    public void OnGet()
    {
        Reservations = _reservationService.GetReservationViews();
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }
    public void OnPostApprove(int reservationId)
    {
        _approvalService.ApproveReservation(reservationId);
        Reservations = _reservationService.GetReservationViews();
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }
    
    public void OnPostReject(int reservationId)
    {
        _approvalService.RejectReservation(reservationId);
        Reservations = _reservationService.GetReservationViews();
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }

    public void OnPostApproveEnrollment(int enrollmentId)
    {
        _enrollmentService.ApproveEnrollment(enrollmentId);
        Reservations = _reservationService.GetReservationViews();
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }

    public void OnPostRejectEnrollment(int enrollmentId)
    {
        _enrollmentService.RejectEnrollment(enrollmentId);
        Reservations = _reservationService.GetReservationViews();
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }
    
}