using BLL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class SetPasswordModel : PageModel
{
    private readonly LoginService _loginService;

    public SetPasswordModel(LoginService loginService)
    {
        _loginService = loginService;
    }

    public void OnGet()
    {
        _loginService.SetPassword(1, "teacher123");
        _loginService.SetPassword(2, "student123");
        _loginService.SetPassword(4, "admin123");
    }
}