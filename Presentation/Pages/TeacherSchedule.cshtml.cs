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

    public IActionResult OnGet()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToPage("/Index");
        }

        Reservations = _reservationService.GetTeacherReservations(userId.Value, Status);

        return Page();
    }
}