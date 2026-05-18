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

    public void OnPost()
    {
        Classrooms = _reservationService.GetAvailableClassrooms();

        try
        {
            var times = SelectedTimeSlot.Split('-');

            var startTime = TimeSpan.Parse(times[0].Trim());
            var endTime = TimeSpan.Parse(times[1].Trim());

            var startDateTime = ReservationDate.Date.Add(startTime);
            var endDateTime = ReservationDate.Date.Add(endTime);

            Console.WriteLine($"Date: {ReservationDate}");
            Console.WriteLine($"Slot: {SelectedTimeSlot}");
            Console.WriteLine($"Start: {startDateTime}");
            Console.WriteLine($"End: {endDateTime}");

            _reservationService.CreateReservation(
                userId: 1,
                classroomId: ClassroomId,
                subject: Subject,
                startDateTime: startDateTime,
                endDateTime: endDateTime
            );

            Message = "Reservation created successfully.";
        }
        catch (Exception ex)
        {
            Message = ex.Message;
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
}
    
