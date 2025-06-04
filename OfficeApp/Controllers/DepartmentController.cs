
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;

namespace OfficeApp.Controllers;

public class DepartmentController : Controller
{
    private readonly AppDbContext _context;

    public DepartmentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_context.Departments.ToList());
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Department department)
    {
        if (ModelState.IsValid)
        {
            _context.Add(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(department);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = _context.Departments.Find(id);
        if (department == null)
        {
            return NotFound();
        }
        return View(department);
    }

    [HttpPost]
    public IActionResult Edit(int id, Department department)
    {
        if (id != department.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(department);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departments.Any(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(department);
    }
}
