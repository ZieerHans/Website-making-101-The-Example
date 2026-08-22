using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EchoesOfGrace.Pages;

public class FinalPrayerModel : PageModel
{
    public void OnGet()
    {
        ViewData["Title"] = "Final Prayer";
        ViewData["Active"] = "FinalPrayer";
    }
}
