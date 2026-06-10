using BLL;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class AdminScheduleModel : PageModel
{
    private readonly ReservationService _reservationService;

    public List<ReservationView> Reservations { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Status { get; set; }

    public AdminScheduleModel(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public void OnGet()
    {
        Reservations = _reservationService.GetAllReservationsByStatus(Status);
    }
}