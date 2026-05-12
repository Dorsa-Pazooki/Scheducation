using BLL;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class TeacherModel : PageModel
{
    private readonly ReservationService _reservationService;

    public List<Classroom> Classrooms { get; set; } = new();

    [BindProperty]
    public int ClassroomId { get; set; }

    [BindProperty]
    public string Subject { get; set; } = "";

    [BindProperty]
    public DateTime ReservationTime { get; set; }

    public string Message { get; set; } = "";

    public TeacherModel(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public void OnGet()
    {
        Classrooms = _reservationService.GetAvailableClassrooms();
    }

    public void OnPost()
    {
        Classrooms = _reservationService.GetAvailableClassrooms();

        _reservationService.CreateReservation(
            userId: 1,
            classroomId: ClassroomId,
            subject: Subject,
            reservationTime: ReservationTime
        );

        Message = $"Reservation created for classroom {ClassroomId}.";
    }
}