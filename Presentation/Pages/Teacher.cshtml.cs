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

    public string Message { get; set; } = "";
    
    [BindProperty]
    public DateTime ReservationDate { get; set; }

    [BindProperty]
    public string SelectedTimeSlot { get; set; } = "";

    public TeacherModel(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public void OnGet()
    {
        Classrooms = _reservationService.GetAvailableClassrooms();
    }

    public IActionResult OnPost()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToPage("/Index");
        }

        Classrooms = _reservationService.GetAvailableClassrooms();

        var selectedDate = ReservationDate;
        var times = SelectedTimeSlot.Split("-");

        var startTime = TimeSpan.Parse(times[0].Trim());
        var endTime = TimeSpan.Parse(times[1].Trim());

        var startDateTime = selectedDate.Date.Add(startTime);
        var endDateTime = selectedDate.Date.Add(endTime);
        
        _reservationService.CreateReservation(
            userId: userId.Value,
            classroomId: ClassroomId,
            subject: Subject,
            startDateTime: startDateTime,
            endDateTime: endDateTime
        );

        Message = "Reservation created successfully.";

        Classrooms = _reservationService.GetAvailableClassrooms();

        return Page();
    }
    
}
    
