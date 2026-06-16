using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class RegisterModel : PageModel
{
    private readonly LoginService _loginService;

    [BindProperty] public string FirstName { get; set; } = "";
    [BindProperty] public string LastName { get; set; } = "";
    [BindProperty] public string Email { get; set; } = "";
    [BindProperty] public string Password { get; set; } = "";

    public string Message { get; set; } = "";

    public RegisterModel(LoginService loginService)
    {
        _loginService = loginService;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        _loginService.RegisterStudent(FirstName, LastName, Email, Password);

        Message = "Account created successfully. You can now log in.";
        return Page();
    }
}