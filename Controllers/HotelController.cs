using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using resa2.Models;

namespace resa2.Controllers;

[Authorize]
public class HotelController : Controller
{
    private readonly AppContext _context;

    public HotelController(AppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var hotels = _context.Hotels.ToList();
        return View(hotels);
    }

    [HttpGet("Hotel/Details/{id}")]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        Hotel? hotel = _context.Hotels.Find(id);
        if (hotel == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(hotel);
    }

    public IActionResult Create()
    {
        ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        // If the model state is not valid, retrieve the validation errors
        var validationErrors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

        // Pass the hotel object and validation errors to the view
        ViewBag.ValidationErrors = validationErrors;
        ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
        return View(hotel);
    }

    [HttpGet("Hotel/Edit/{id}")]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        Hotel? hotel = _context.Hotels.Find(id);
        if (hotel == null)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
        return View(hotel);
    }

    [HttpPost("Hotel/Edit/{id}/save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSave(Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            _context.Update(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        // If the model state is not valid, retrieve the validation errors
        var validationErrors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

        // Pass the hotel object and validation errors to the view
        ViewBag.ValidationErrors = validationErrors;
        ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
        return RedirectToAction(nameof(Edit), new {id = hotel.Id});
    }

    [HttpGet("Hotel/Delete/{id}")]
    public async Task<IActionResult> Delete(Hotel hotel)
    {
        _context.Remove(hotel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
