using System.ComponentModel.DataAnnotations;

namespace EchoesOfGrace.Models;

public class ReflectionForm
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(80, ErrorMessage = "Please keep the name under 80 characters.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Please write a short reflection before submitting.")]
    [MinLength(12, ErrorMessage = "Please write a little more (12 characters minimum).")]
    [StringLength(2000, ErrorMessage = "Please keep the reflection under 2000 characters.")]
    public string Reflection { get; set; } = "";

    [StringLength(2000, ErrorMessage = "Please keep the lesson under 2000 characters.")]
    public string? Lesson { get; set; }
}
