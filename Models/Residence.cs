using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc; // Required for [Remote]

namespace AirBB.Models;

public class Residence
{
    public int ResidenceId { get; set; }

    [Required(ErrorMessage = "Property Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Name can only contain alphanumeric characters and spaces.")]
    public string Name { get; set; } = string.Empty;

    
    public string ResidencePicture { get; set; } = "default.jpg";

    [Required(ErrorMessage = "Please select a Location.")]
    public int LocationId { get; set; }
    public Location? Location { get; set; }

    
    [Required(ErrorMessage = "Owner ID is required.")]
    [Remote("CheckOwner", "Validation", ErrorMessage = "Owner ID does not exist or is not an Owner.")]
    public int OwnerId { get; set; }
    
   
    [ForeignKey("OwnerId")]
    public Client? Owner { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Guests must be at least 1.")]
    public int GuestNumber { get; set; }

    
    [Required]
    [Range(1, 50, ErrorMessage = "Bedrooms must be at least 1.")]
    public int BedroomNumber { get; set; }

    
    [Required]
    [RegularExpression(@"^[0-9]+(\.5)?$", ErrorMessage = "Bathrooms must be an integer or end in .5")]
    
    [Range(0.5, 50, ErrorMessage = "Bathrooms must be at least 0.5.")]
    public double BathroomNumber { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 100000, ErrorMessage = "Price must be greater than 0.")]
    public decimal PricePerNight { get; set; }

    
    [Required]
    [BuiltYear(150, ErrorMessage = "Built Year must be in the past and no older than 150 years.")]
    public int BuiltYear { get; set; }
}