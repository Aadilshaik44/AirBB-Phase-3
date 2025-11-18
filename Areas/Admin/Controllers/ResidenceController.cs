using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirBB.Models;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidenceController : Controller
    {
        private readonly AirBnbContext _context;

        public ResidenceController(AirBnbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var residences = await _context.Residences
                                         .Include(r => r.Location)
                                         .Include(r => r.Owner) // <-- FIX: This line is added
                                         .OrderBy(r => r.Name)
                                         .ToListAsync();
            return View(residences);
        }
        
        
        private void LoadLocations(int? selectedId = null)
        {
            ViewBag.Locations = new SelectList(_context.Locations.OrderBy(l => l.Name), 
                "LocationId", "Name", selectedId);
        }

        
        private void LoadOwners(int? selectedId = null)
        {
            ViewBag.Owners = new SelectList(_context.Clients
                .Where(c => c.UserType.ToLower() == "owner") 
                .OrderBy(c => c.Name), 
                "UserId", "Name", selectedId); 
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            LoadLocations();
            LoadOwners();
            ModelState.Clear(); 
            
            return View(new Residence());
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(Residence residence)
        {
            if (ModelState.IsValid)
            {
                _context.Residences.Add(residence);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Residence added successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "Add";
            LoadLocations(residence.LocationId);
            LoadOwners(residence.OwnerId);
            return View(residence);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var residence = await _context.Residences.FindAsync(id);
            if (residence == null)
            {
                return NotFound();
            }

            ViewBag.Action = "Edit";
            LoadLocations(residence.LocationId);
            LoadOwners(residence.OwnerId);
            return View(residence);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(Residence residence)
        {
            if (ModelState.IsValid)
            {
                _context.Update(residence);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Residence updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Action = "Edit";
            LoadLocations(residence.LocationId);
            LoadOwners(residence.OwnerId);
            return View(residence);
        }

       
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var residence = await _context.Residences
                                        .Include(r => r.Location)
                                        .Include(r => r.Owner) // <-- FIX: This line is added
                                        .FirstOrDefaultAsync(r => r.ResidenceId == id);
            if (residence == null)
            {
                return NotFound();
            }
            return View(residence);
        }

        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residence = await _context.Residences.FindAsync(id);
            if (residence != null)
            {
                _context.Residences.Remove(residence);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Residence deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}