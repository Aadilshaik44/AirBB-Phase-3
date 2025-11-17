using System.ComponentModel.DataAnnotations;

namespace AirBB.Models;

public class Location
{
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Location Name is required.")]
    [StringLength(60, ErrorMessage = "Name cannot be longer than 60 characters.")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Residence> Residences { get; set; } = new List<Residence>();
}