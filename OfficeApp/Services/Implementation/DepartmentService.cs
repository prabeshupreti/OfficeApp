using OfficeApp.Models;
using OfficeApp.Services.Abstraction;

namespace OfficeApp.Services.Implementation;

public class DepartmentService : IDepartmentService
{

    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }

    public void AddDepartment(Department department)
    {
        _context.Departments.Add(department);
        _context.SaveChanges();
    }

    public void DeleteDepartment(Department department)
    {
        _context.Departments.Remove(department);
        _context.SaveChanges();
    }

    public bool DepartmentExists(int? Id)
    {
        return _context.Departments.Any(x => x.Id == Id);
    }

    public Department GetDepartmentById(int? Id)
    {
        return _context.Departments.Find(Id);
    }

    public List<Department> GetDepartments()
    {
        return _context.Departments.ToList();
    }

    public void UpdateDepartment(Department department)
    {
        _context.Departments.Update(department);
        _context.SaveChanges();
    }
}
