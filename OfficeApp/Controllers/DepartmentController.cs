
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;
using OfficeApp.Services.Abstraction;
using OfficeApp.Services.Implementation;

namespace OfficeApp.Controllers;

[Authorize]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_departmentService.GetDepartments());
    }

    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = _departmentService.GetDepartmentById(id);
        if (department == null)
        {
            return NotFound();
        }

        return View(department);
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
            _departmentService.AddDepartment(department);
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

        var department = _departmentService.GetDepartmentById(id);

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
                _departmentService.UpdateDepartment(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_departmentService.DepartmentExists(id))
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

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = _departmentService.GetDepartmentById(id);
        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }


    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var department = _departmentService.GetDepartmentById(id);

        if (department != null)
        {
            _departmentService.DeleteDepartment(department);
        }

        return RedirectToAction(nameof(Index));
    }
}
