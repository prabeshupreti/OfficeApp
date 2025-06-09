using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeApp;
using OfficeApp.Models;
using OfficeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(AppDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Employee
        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }

        // GET: Employee/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees
                .FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel.Photo != null && employeeViewModel.Photo.Length > 0)
            {
                // Create a unique filename (optional)
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(employeeViewModel.Photo.FileName);

                // Determine the path to save file (e.g., wwwroot/ProfilePhotos)
                var uploadsFolder = $@"{_webHostEnvironment.WebRootPath}\ProfilePhotos\";
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = $@"{uploadsFolder}{fileName}";

                Employee employee = new Employee
                {
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Contact = employeeViewModel.Contact,
                    Address = employeeViewModel.Address,
                };

                employee.PhotoPath = filePath.Split("wwwroot")[1];

                if (!ModelState.IsValid)
                {
                    return View(employeeViewModel);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    employeeViewModel.Photo.CopyTo(fileStream);
                }

                _context.Employees.Add(employee);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(nameof(employeeViewModel.Photo), "Please, add a photo.");
            return View(employeeViewModel);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            UpdateEmployeeViewModel updateEmployeeViewModel = new UpdateEmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Contact = employee.Contact,
                Address = employee.Address,
                PhotoPath = employee.PhotoPath
            };

            return View(updateEmployeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, UpdateEmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
            {
                return NotFound();
            }
            try
            {
                if (employeeViewModel.Photo != null && employeeViewModel.Photo.Length > 0)
                {
                    // Create a unique filename (optional)
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(employeeViewModel.Photo.FileName);

                    // Determine the path to save file (e.g., wwwroot/ProfilePhotos)
                    var uploadsFolder = $@"{_webHostEnvironment.WebRootPath}\ProfilePhotos\";
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = $@"{uploadsFolder}{fileName}";

                    Employee employee = new Employee
                    {
                        Id = employeeViewModel.Id,
                        FirstName = employeeViewModel.FirstName,
                        LastName = employeeViewModel.LastName,
                        Contact = employeeViewModel.Contact,
                        Address = employeeViewModel.Address,
                    };

                    employee.PhotoPath = filePath.Split("wwwroot")[1];

                    if (!ModelState.IsValid)
                    {
                        return View(employeeViewModel);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        employeeViewModel.Photo.CopyTo(fileStream);
                    }

                    string file = @$"{_webHostEnvironment.WebRootPath}{employeeViewModel.PhotoPath}";

                    if (System.IO.File.Exists(file))

                        System.IO.File.Delete(file);

                    _context.Update(employee);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employeeViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View(employeeViewModel);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees
                .FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            _context.SaveChanges();


            // Determine the path to save file (e.g., wwwroot/ProfilePhotos)
            string file = @$"{_webHostEnvironment.WebRootPath}{employee.PhotoPath}";

            if (System.IO.File.Exists(file))

                System.IO.File.Delete(file);

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
