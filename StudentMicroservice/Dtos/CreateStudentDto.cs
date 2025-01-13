using System.ComponentModel.DataAnnotations;

namespace StudentMicroservice.Dtos;

public class CreateStudentDto
{
    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "State of residence is required")]
    [StringLength(100)]
    public required string StateOfResidence { get; set; }

    [Required(ErrorMessage = "LGA is required")]
    [StringLength(100)]
    public required string Lga { get; set; }
}
