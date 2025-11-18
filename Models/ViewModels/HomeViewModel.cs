using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirBB.Models;

public class HomeViewModel
{
    
    public string ActiveLocationId { get; set; } = "All";   
    public string ActiveGuests { get; set; } = "1";
    public string ActiveStart { get; set; } = "";           
    public string ActiveEnd { get; set; } = "";

    
    public List<SelectListItem> Locations { get; set; } = new();
    public List<Residence> Residences { get; set; } = new();

    
    public List<Reservation> Reservations { get; set; } = new();
    public int ReservationsCount => Reservations?.Count ?? 0;
}
