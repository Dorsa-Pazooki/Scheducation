using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class LoginModel : PageModel
{
    private readonly LoginService _loginService;

    [BindProperty]
    public string Email { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    public string ErrorMessage { get; set; } = "";

    public LoginModel(LoginService loginService)
    {
        _loginService = loginService;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        var user = _loginService.Login(Email, Password);

        if (user == null)
        {
            ErrorMessage = "Invalid email or password.";
            return Page();
        }

        HttpContext.Session.SetInt32("UserId", user.UserId);
        HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);

        var role = _loginService.GetUserRole(user.UserId);

        HttpContext.Session.SetInt32("UserId", user.UserId);
        HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
        HttpContext.Session.SetString("Role", role ?? "");

        if (role == "Teacher")
        {
            return RedirectToPage("/Teacher");
        }

        if (role == "Student")
        {
            return RedirectToPage("/Student");
        }

        if (role == "Admin")
        {
            return RedirectToPage("/Admin");
        }

        ErrorMessage = "User role was not found.";
        return Page();
    }
}