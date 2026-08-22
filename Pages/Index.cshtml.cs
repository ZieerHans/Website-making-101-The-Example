using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EchoesOfGrace.Pages;

public class IndexModel : PageModel
{
    public void OnGet()
    {
        ViewData["Title"] = "Home";
        ViewData["Active"] = "Home";
    }
}
