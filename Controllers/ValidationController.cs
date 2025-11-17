using AirBB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers;

public class ValidationController : Controller
{
    private readonly AirBnbContext _context;

    public ValidationController(AirBnbContext context)
    {
        _context = context;
    }

    // Remote validation action
    public IActionResult CheckOwner(int ownerId)
    {
        // Check if user exists AND has UserType "Owner"
        bool exists = _context.Clients.Any(c => c.UserId == ownerId && c.UserType == "Owner");
        
        if (!exists)
        {
            return Json($"Owner ID {ownerId} is invalid or not registered as an Owner.");
        }
        
        return Json(true);
    }
}