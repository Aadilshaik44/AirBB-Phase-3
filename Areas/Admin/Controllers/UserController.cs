using Microsoft.AspNetCore.Mvc;
using AirBB.Models;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AirBnbContext _context;
        public UserController(AirBnbContext context) => _context = context;

        // GET: /Admin/User/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: /Admin/User/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View(new Client());
        }

        // POST: /Admin/User/Add
        [HttpPost]
        public async Task<IActionResult> Add(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                TempData["Message"] = "User added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: /Admin/User/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: /Admin/User/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                TempData["Message"] = "User updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: /Admin/User/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: /Admin/User/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                TempData["Message"] = "User deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}