using Microsoft.AspNetCore.Mvc;
using AirBB.Models;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly AirBnbContext _context;
        public LocationController(AirBnbContext context) => _context = context;

       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Locations.ToListAsync());
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            return View(new Location());
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Location added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

       
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Update(location);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Location updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Location deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}