using System.Net;
using System.Net.Mail;
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

    public async Task<IActionResult> OnPostAsync()
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

        try
        {
            await SendReflectionEmailAsync();
        }
        catch (Exception)
        {
            // Credentials missing/misconfigured, or Gmail rejected the send.
            // Fail visibly instead of silently pretending it worked.
            ModelState.AddModelError(string.Empty, "Something went wrong sending your reflection. Please try again in a moment.");
            Submitted = false;
            return Page();
        }

        Submitted = true;
        SubmittedName = Input.Name.Trim();
        return Page();
    }

    private async Task SendReflectionEmailAsync()
    {
        // Credentials are read from environment variables set in Render's
        // dashboard — never hardcoded here, and never committed to GitHub.
        var gmailAddress = Environment.GetEnvironmentVariable("GMAIL_ADDRESS");
        var gmailAppPassword = Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD");

        if (string.IsNullOrWhiteSpace(gmailAddress) || string.IsNullOrWhiteSpace(gmailAppPassword))
        {
            throw new InvalidOperationException("Email credentials are not configured.");
        }

        using var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(gmailAddress, gmailAppPassword),
            EnableSsl = true
        };

        using var mail = new MailMessage
        {
            From = new MailAddress(gmailAddress, "Echoes of Grace"),
            Subject = $"New reflection from {Input.Name}",
            Body =
                $"Name: {Input.Name}\n\n" +
                $"Reflection:\n{Input.Reflection}\n\n" +
                $"Lesson learned from St. Augustine:\n{(string.IsNullOrWhiteSpace(Input.Lesson) ? "(not provided)" : Input.Lesson)}"
        };
        mail.To.Add(gmailAddress);

        await client.SendMailAsync(mail);
    }
}
