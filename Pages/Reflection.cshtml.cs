using EchoesOfGrace.Data;
using EchoesOfGrace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EchoesOfGrace.Pages;

public class ReflectionModel : PageModel
{
    private readonly ReflectionStore _store;

    public ReflectionModel(ReflectionStore store)
    {
        _store = store;
    }

    [BindProperty]
    public ReflectionForm Input { get; set; } = new();

    public bool Submitted { get; private set; }
    public string SubmittedName { get; private set; } = "";
    public int TotalCount { get; private set; }

    public void OnGet()
    {
        ViewData["Title"] = "Leave a Reflection";
        ViewData["Active"] = "Reflection";
        TotalCount = _store.Count;
    }

    public IActionResult OnPost()
    {
        ViewData["Title"] = "Leave a Reflection";
        ViewData["Active"] = "Reflection";

        if (!ModelState.IsValid)
        {
            Submitted = false;
            TotalCount = _store.Count;
            return Page();
        }

        _store.Add(Input.Name.Trim(), Input.Reflection.Trim(), Input.Lesson?.Trim());

        Submitted = true;
        SubmittedName = Input.Name.Trim();
        TotalCount = _store.Count;
        return Page();
    }
}
