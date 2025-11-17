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

        // GET: /Admin/Residence/Index
        public async Task<IActionResult> Index()
        {
            var residences = await _context.Residences
                                    .Include(r => r.Location)
                                    .OrderBy(r => r.Name)
                                    .ToListAsync();
            return View(residences);
        }
        
        private void LoadLocations(int? selectedId = null)
        {
            ViewBag.Locations = new SelectList(_context.Locations.OrderBy(l => l.Name), 
                "LocationId", "Name", selectedId);
        }

        // GET: /Admin/Residence/Add
        [HttpGet]
        public IActionResult Add()
        {
            LoadLocations();
            return View(new Residence() { BuiltYear = DateTime.Now.Year }); // Default year
        }

        // POST: /Admin/Residence/Add
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

            // Reload locations if validation fails
            LoadLocations(residence.LocationId);
            return View(residence);
        }

        // GET: /Admin/Residence/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var residence = await _context.Residences.FindAsync(id);
            if (residence == null)
            {
                return NotFound();
            }
            LoadLocations(residence.LocationId);
            return View(residence);
        }

        // POST: /Admin/Residence/Edit/{id}
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
            
            // Reload locations if validation fails
            LoadLocations(residence.LocationId);
            return View(residence);
        }

        // GET: /Admin/Residence/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var residence = await _context.Residences
                                    .Include(r => r.Location)
                                    .FirstOrDefaultAsync(r => r.ResidenceId == id);
            if (residence == null)
            {
                return NotFound();
            }
            return View(residence);
        }

        // POST: /Admin/Residence/Delete/{id}
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