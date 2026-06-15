using BLL;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class TeacherScheduleModel : PageModel
{
    private readonly ReservationService _reservationService;

    public List<ReservationView> Reservations { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Status { get; set; }

    public TeacherScheduleModel(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public void OnGet()
    {
        Reservations = _reservationService.GetTeacherReservations(1, Status);
    }
}