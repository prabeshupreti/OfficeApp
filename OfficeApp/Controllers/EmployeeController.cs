using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeApp;
using OfficeApp.Models;
using OfficeApp.Services.Abstraction;
using OfficeApp.Services.Implementation;
using OfficeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IWebHostEnvironmentService _webHostEnvironmentService;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IWebHostEnvironmentService webHostEnvironmentService,
            IEmployeeService employeeService)
        {
            _webHostEnvironmentService = webHostEnvironmentService;
            _employeeService = employeeService;
        }

        // GET: Employee
        public IActionResult Index()
        {
            return View(_employeeService.GetEmployees());
        }

        // GET: Employee/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetEmployeeById(id);
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

                Employee employee = new Employee
                {
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Contact = employeeViewModel.Contact,
                    Address = employeeViewModel.Address,
                    DepartmentId = employeeViewModel.DepartmentId
                };

                var FilePath = _webHostEnvironmentService.GetFilePath(fileName);

                employee.PhotoPath = FilePath.RelativePath;

                if (!ModelState.IsValid)
                {
                    return View(employeeViewModel);
                }

                _employeeService.AddEmployee(employee);
                _webHostEnvironmentService.AddEmployeeProfilePhoto(employeeViewModel.Photo, FilePath.AbsolutePath);

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

            var employeeExists = _employeeService.EmployeeExists(id);
            if (!employeeExists)
            {
                return NotFound();
            }

            var employee = _employeeService.GetEmployeeById(id);

            UpdateEmployeeViewModel updateEmployeeViewModel = new UpdateEmployeeViewModel
            {
                Id = employee!.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Contact = employee.Contact,
                Address = employee.Address,
                PhotoPath = employee.PhotoPath,
                DepartmentId = employee.DepartmentId,
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
                var employee = new Employee
                {
                    Id = employeeViewModel.Id,
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Contact = employeeViewModel.Contact,
                    Address = employeeViewModel.Address,
                    DepartmentId = employeeViewModel.DepartmentId,
                };

                if (employeeViewModel.Photo != null && employeeViewModel.Photo.Length > 0)
                {
                    // Create a unique filename (optional)
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(employeeViewModel.Photo.FileName);

                    // Determine the path to save file (e.g., wwwroot/ProfilePhotos)
                    var FilePath = _webHostEnvironmentService.GetFilePath(fileName);

                    string AbsoluteFilePath = FilePath.AbsolutePath;

                    string RelativeFilePath = FilePath.RelativePath;

                    if (!ModelState.IsValid)
                    {
                        return View(employeeViewModel);
                    }

                    employee.PhotoPath = RelativeFilePath;

                    _webHostEnvironmentService.AddEmployeeProfilePhoto(employeeViewModel.Photo, AbsoluteFilePath);

                    _webHostEnvironmentService.DeleteEmployeeProfilePhoto(employeeViewModel.PhotoPath);
                }
                else
                {
                    employee.PhotoPath = employeeViewModel.PhotoPath;
                }

                _employeeService.UpdateEmployee(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_employeeService.EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetEmployeeById(id);
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
            var employee = _employeeService.GetEmployeeById(id);
            if (employee != null)
                _employeeService.DeleteEmployee(employee);


            // Determine the path to save file (e.g., wwwroot/ProfilePhotos)
            _webHostEnvironmentService.DeleteEmployeeProfilePhoto(employee!.PhotoPath);

            return RedirectToAction(nameof(Index));
        }
    }
}
