using BLL;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class StudentScheduleModel : PageModel
{
    private readonly EnrollmentService _enrollmentService;

    public List<EnrollmentView> Enrollments { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Status { get; set; }

    public StudentScheduleModel(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public void OnGet()
    {
        Enrollments = _enrollmentService.GetStudentEnrollments(
            studentUserId: 2,
            status: Status
        );
    }
}