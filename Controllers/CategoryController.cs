using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using resa2.Models;

namespace resa2.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly AppContext _context;

    public CategoryController(AppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    [HttpGet("Category/Details/{id}")]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        Category? category = _context.Categories.Find(id);
        if (category == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
        return View(category);
    }

    [HttpGet("Category/Edit/{id}")]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        Category? category = _context.Categories.Find(id);
        if (category == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    [HttpPost("Category/Edit/{id}/save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSave(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        // If the model state is not valid, retrieve the validation errors
        var validationErrors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

        // Pass the category object and validation errors to the view
        ViewBag.ValidationErrors = validationErrors;
        return RedirectToAction(nameof(Edit), new {id = category.Id});
    }

    [HttpGet("Category/Delete/{id}")]
    public async Task<IActionResult> Delete(Category category)
    {
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
