using System.ComponentModel.DataAnnotations;

namespace AirBB.Models;

public class Client
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(60, ErrorMessage = "Name cannot be longer than 60 characters.")]
    public string Name { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string? PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DOB { get; set; }

    // New Property for Phase 3
    [Required(ErrorMessage = "Please select a user type.")]
    public string UserType { get; set; } = "Client"; // Options: "Owner", "Admin", "Client"
}