using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers;

public class ResidenceController : Controller
{
    private readonly AirBnbContext _ctx;
    public ResidenceController(AirBnbContext ctx) => _ctx = ctx;

    
    [HttpGet]
    public IActionResult Index()
    {
        var sess = new AirBBSession(HttpContext.Session);

        
        if (sess.GetReservationIds().Count == 0)
        {
            var jar = new AirBBCookies(Request.Cookies, Response.Cookies);
            sess.SetReservationIds(jar.GetReservationIds());
        }

       
        var model = new HomeViewModel
        {
            ActiveLocationId = sess.GetLoc(),
            ActiveGuests     = sess.GetGuests(),
            ActiveStart      = sess.GetStart(),
            ActiveEnd        = sess.GetEnd(),
            Locations        = BuildLocationList()
        };

        model.Residences = ApplyFilter(model);
        model.Reservations = GetReservationsForBadge(sess.GetReservationIds());
        return View("Index", model);
    }

    
    [HttpPost]
    public IActionResult Filter(HomeViewModel model)
    {
        var sess = new AirBBSession(HttpContext.Session);
        sess.SetFilters(model.ActiveLocationId, model.ActiveGuests, model.ActiveStart, model.ActiveEnd);
        return RedirectToAction(nameof(Index));
    }

    
    [HttpGet]
    public IActionResult Details(int id)
    {
        var res = _ctx.Residences.Include(r => r.Location)
                                 .FirstOrDefault(r => r.ResidenceId == id);
        if (res == null) return RedirectToAction(nameof(Index));

        
        var sess = new AirBBSession(HttpContext.Session);
        ViewData["loc"] = sess.GetLoc();
        ViewData["gu"]  = sess.GetGuests();
        ViewData["st"]  = sess.GetStart();
        ViewData["en"]  = sess.GetEnd();

        return View(res);
    }

    
    private List<SelectListItem> BuildLocationList()
    {
        var items = _ctx.Locations.OrderBy(l => l.Name)
            .Select(l => new SelectListItem { Text = l.Name, Value = l.LocationId.ToString() })
            .ToList();
        items.Insert(0, new SelectListItem { Text = "All", Value = "All" });
        return items;
    }

    private List<Residence> ApplyFilter(HomeViewModel m)
    {
        IQueryable<Residence> q = _ctx.Residences
            .Include(r => r.Location)
            .OrderBy(r => r.Location!.Name).ThenBy(r => r.Name);

        if (int.TryParse(m.ActiveLocationId, out var locId))
            q = q.Where(r => r.LocationId == locId);

        if (int.TryParse(m.ActiveGuests, out var guests) && guests > 1)
            q = q.Where(r => r.GuestNumber >= guests);

        if (DateTime.TryParse(m.ActiveStart, out var s) && DateTime.TryParse(m.ActiveEnd, out var e))
        {
            
            var blocked = _ctx.Reservations
                .Where(rv => rv.ReservationStartDate <= e && rv.ReservationEndDate >= s)
                .Select(rv => rv.ResidenceId)
                .Distinct();

            q = q.Where(r => !blocked.Contains(r.ResidenceId));
        }

        return q.ToList();
    }

    private List<Reservation> GetReservationsForBadge(List<int> ids) =>
        ids.Count == 0
            ? new()
            : _ctx.Reservations.Include(r => r.Residence)
                  .Where(r => ids.Contains(r.ReservationId))
                  .ToList();
}
