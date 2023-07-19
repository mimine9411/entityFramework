using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using resa2.Models;

namespace resa2.Controllers;

[Authorize]
public class RoomController : Controller
{
    private readonly AppContext _context;

    public RoomController(AppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var rooms = _context.Rooms.ToList();
        return View(rooms);
    }

    [HttpGet("Room/Details/{id}")]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        Room? room = _context.Rooms.Find(id);
        if (room == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    public IActionResult Create()
    {
        ViewBag.hotels = new SelectList(_context.Hotels, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Room room)
    {
        if (ModelState.IsValid)
        {
            _context.Add(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var validationErrors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        ViewBag.ValidationErrors = validationErrors;
        ViewBag.hotels = new SelectList(_context.Hotels, "Id", "Name");
        return View(room);
    }

    [HttpGet("Room/Edit/{id}")]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        Room? room = _context.Rooms.Find(id);
        if (room == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    [HttpPost("Room/Edit/{id}/save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSave(Room room)
    {
        if (ModelState.IsValid)
        {
            _context.Update(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        var validationErrors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        ViewBag.ValidationErrors = validationErrors;
        return RedirectToAction(nameof(Edit), new {id = room.Id});
    }

    [HttpGet("Room/Delete/{id}")]
    public async Task<IActionResult> Delete(Room room)
    {
        _context.Remove(room);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
