using System.ComponentModel.DataAnnotations;

namespace AirBB.Models;

public class Client
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(60, ErrorMessage = "Name cannot be longer than 60 characters.")]
    public string Name { get; set; } = string.Empty;

    
    [Required(ErrorMessage = "Phone Number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string PhoneNumber { get; set; } = string.Empty;

   
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    
    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; }

    
    [Required(ErrorMessage = "Please select a user type.")]
    public string UserType { get; set; } = string.Empty; 

    public ICollection<Residence> Residences { get; set; } = new List<Residence>();
}