using BLL;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class AdminModel : PageModel
{
    private readonly ReservationService _reservationService;
    private readonly ReservationApprovalService _approvalService;

    public List<ReservationView> Reservations { get; set; } = new();

    public AdminModel(
        ReservationService reservationService,
        ReservationApprovalService approvalService)
    {
        _reservationService = reservationService;
        _approvalService = approvalService;
    }

    public void OnGet()
    {
        Reservations = _reservationService.GetReservationViews();
    }

    public void OnPostApprove(int reservationId)
    {
        _approvalService.ApproveReservation(reservationId);
        Reservations = _reservationService.GetReservationViews();
    }

    public void OnPostReject(int reservationId)
    {
        _approvalService.RejectReservation(reservationId);
        Reservations = _reservationService.GetReservationViews();
    }
}