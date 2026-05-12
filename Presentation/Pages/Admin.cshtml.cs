using BLL;
using Domain.Entities;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class AdminModel : PageModel
{
    private readonly ReservationService _reservationService;

    public List<ReservationView> Reservations { get; set; } = new();

    public AdminModel(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public void OnGet()
    {
        Reservations = _reservationService.GetReservationViews();
    }
}