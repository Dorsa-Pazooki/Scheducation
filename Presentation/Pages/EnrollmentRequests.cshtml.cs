using BLL;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class EnrollmentRequestsModel : PageModel
{
    private readonly EnrollmentService _enrollmentService;

    public List<EnrollmentView> EnrollmentRequests { get; set; } = new();

    public EnrollmentRequestsModel(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public void OnGet()
    {
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }

    public void OnPostApprove(int enrollmentId)
    {
        _enrollmentService.ApproveEnrollment(enrollmentId);
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }

    public void OnPostReject(int enrollmentId)
    {
        _enrollmentService.RejectEnrollment(enrollmentId);
        EnrollmentRequests = _enrollmentService.GetEnrollmentRequests();
    }
}