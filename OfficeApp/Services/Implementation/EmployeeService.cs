using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;
using OfficeApp.Services.Abstraction;

namespace OfficeApp.Services.Implementation;

public class EmployeeService : IEmployeeService
{
    private readonly IWebHostEnvironment _environment;

    private readonly AppDbContext _context;

    public EmployeeService(IWebHostEnvironment environment, AppDbContext context)
    {
        _environment = environment;
        _context = context;
    }

    public void AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void DeleteEmployee(Employee employee)
    {
        _context.Employees.Remove(employee);
        _context.SaveChanges();
    }

    public bool EmployeeExists(int? Id)
    {
        return _context.Employees.Any(x => x.Id == Id);
    }

    public IQueryable<Employee> GetAllEmployees()
    {
        return _context.Employees.Include(x => x.Department);
    }

    public Employee GetEmployeeById(int? Id)
    {
        return _context.Employees.Find(Id);
    }

    public List<Employee> GetEmployees()
    {
        return _context.Employees.Include(x => x.Department).ToList();
    }

    public void UpdateEmployee(Employee employee)
    {
        _context.Update(employee);
        _context.SaveChanges();
    }
}
