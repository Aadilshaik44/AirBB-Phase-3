using System.ComponentModel.DataAnnotations;

namespace AirBB.Models;

public class Location
{
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Location Name is required.")]
    [StringLength(60, ErrorMessage = "Name cannot be longer than 60 characters.")]
    
    [RegularExpression(@"^[a-zA-Z][a-zA-Z\s.-]*$", ErrorMessage = "Name must start with a letter and can only contain letters, spaces, periods, or hyphens.")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Residence> Residences { get; set; } = new List<Residence>();
}