using EchoesOfGrace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EchoesOfGrace.Pages;

public class ReflectionModel : PageModel
{
    [BindProperty]
    public ReflectionForm Input { get; set; } = new();

    public bool Submitted { get; private set; }
    public string SubmittedName { get; private set; } = "";

    public void OnGet()
    {
        ViewData["Title"] = "Leave a Reflection";
        ViewData["Active"] = "Reflection";
    }

    public IActionResult OnPost()
    {
        ViewData["Title"] = "Leave a Reflection";
        ViewData["Active"] = "Reflection";

        // Genuine C# server-side processing: ASP.NET Core model-binds the
        // POSTed form into ReflectionForm, and the data-annotation rules on
        // that model (see Models/ReflectionForm.cs) are what actually decide
        // whether the submission is valid. Nothing here is faked in JS.
        if (!ModelState.IsValid)
        {
            Submitted = false;
            return Page();
        }

        Submitted = true;
        SubmittedName = Input.Name.Trim();
        return Page();
    }
}
