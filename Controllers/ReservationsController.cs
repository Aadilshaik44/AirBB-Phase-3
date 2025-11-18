using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers;

public class ReservationsController : Controller
{
    private readonly AirBnbContext _ctx;
    public ReservationsController(AirBnbContext ctx) => _ctx = ctx;

    [HttpGet]
    public IActionResult Index()
    {
        var sess = new AirBBSession(HttpContext.Session);
        var ids = sess.GetReservationIds();

        var list = (ids.Count == 0)
            ? new List<Reservation>()
            : _ctx.Reservations.Include(r => r.Residence).ThenInclude(x => x!.Location)
                  .Where(r => ids.Contains(r.ReservationId))
                  .OrderByDescending(r => r.ReservationId)
                  .ToList();

        return View(list);
    }


    [HttpPost]
    public IActionResult Reserve(int id, string start, string end)
    {
        if (!DateTime.TryParse(start, out var sdt) || !DateTime.TryParse(end, out var edt) || sdt > edt)
        {
            TempData["message"] = "Please select a valid date range.";
            return RedirectToAction("Details", "Residence", new { id });
        }

        bool overlap = _ctx.Reservations.Any(r => r.ResidenceId == id &&
                                                  r.ReservationStartDate <= edt &&
                                                  r.ReservationEndDate >= sdt);
        if (overlap)
        {
            TempData["message"] = "Sorry, those dates are unavailable.";
            return RedirectToAction("Details", "Residence", new { id });
        }

        var res = new Reservation
        {
            ResidenceId = id,
            ReservationStartDate = sdt.Date,
            ReservationEndDate   = edt.Date
        };
        _ctx.Reservations.Add(res);
        _ctx.SaveChanges();

        
        var jar = new AirBBCookies(Request.Cookies, Response.Cookies);
        var ids = jar.GetReservationIds();
        ids.Add(res.ReservationId);
        jar.SaveReservationIds(ids);

        var sess = new AirBBSession(HttpContext.Session);
        sess.SetReservationIds(ids);

        TempData["message"] = "Reservation confirmed!";
        return RedirectToAction("Index", "Residence"); // PRG back to filter page
    }

    [HttpPost]
    public IActionResult Cancel(int id)
    {
        var r = _ctx.Reservations.Find(id);
        if (r != null)
        {
            _ctx.Reservations.Remove(r);
            _ctx.SaveChanges();

            
            var jar = new AirBBCookies(Request.Cookies, Response.Cookies);
            var ids = jar.GetReservationIds();
            ids.Remove(id);
            jar.SaveReservationIds(ids);

            new AirBBSession(HttpContext.Session).SetReservationIds(ids);
            TempData["message"] = "Reservation canceled.";
        }
        return RedirectToAction(nameof(Index));
    }
}
