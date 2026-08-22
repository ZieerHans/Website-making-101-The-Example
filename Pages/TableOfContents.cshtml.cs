using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EchoesOfGrace.Pages;

public class TableOfContentsModel : PageModel
{
    public void OnGet()
    {
        ViewData["Title"] = "Table of Contents";
        ViewData["Active"] = "TableOfContents";
    }
}
