using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using resa2.Models;

namespace resa2.Controllers;

public class UserController : Controller
{
    private readonly AppContext _context;

    public UserController( AppContext context)
    {
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        ViewBag.users = users;
        return View();
    }

    [Authorize]
    [HttpGet("User/Details/{id}")]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        User? user = _context.Users.Find(id);
        if (user == null)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewBag.user = user;
        return View();
    }

  
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            user.Password = HashPassword(user.Password);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }
    
    [Authorize]
    [HttpGet("User/Edit/{id}")]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
        User? user = _context.Users.Find(id);
        if (user == null)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewBag.user = user;
        return View(user);
    }

    [Authorize]
    [HttpPost("User/Edit/{id}/save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSave(int id, User user)
    {
        if (ModelState.IsValid)
        {
            user.Password = HashPassword(user.Password);
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        // If the model state is not valid, retrieve the validation errors
        var validationErrors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

        // Pass the user object and validation errors to the view
        ViewBag.ValidationErrors = validationErrors;
        return RedirectToAction(nameof(Edit), new {id = id});
    }
    
    
    [Authorize]
    [HttpGet("User/Delete/{id}")]
    public async Task<IActionResult> Delete(User user)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public string HashPassword(string password)
    {
        // generate a 128-bit salt using a secure PRNG
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return $"{ Convert.ToBase64String(salt) }:{ hashed }";
    }
    
}
