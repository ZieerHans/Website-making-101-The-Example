using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EchoesOfGrace.Pages;

public class ConfessionsModel : PageModel
{
    public void OnGet()
    {
        ViewData["Title"] = "Confessions";
        ViewData["Active"] = "Confessions";
    }
}
