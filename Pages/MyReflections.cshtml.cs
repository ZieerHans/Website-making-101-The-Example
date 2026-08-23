using EchoesOfGrace.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EchoesOfGrace.Pages;

public class MyReflectionsModel : PageModel
{
    private readonly ReflectionStore _store;

    public MyReflectionsModel(ReflectionStore store)
    {
        _store = store;
    }

    public IReadOnlyList<SubmittedReflection> Entries { get; private set; } = new List<SubmittedReflection>();

    public void OnGet()
    {
        ViewData["Title"] = "Review Reflections";
        ViewData["Active"] = "MyReflections";
        Entries = _store.GetAll();
    }
}
