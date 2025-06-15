using OfficeApp.Models;

namespace OfficeApp.Services.Abstraction;

public interface IDepartmentService
{
    void AddDepartment(Department department);
    void DeleteDepartment(Department department);
    List<Department> GetDepartments();
    Department GetDepartmentById(int? Id);
    void UpdateDepartment(Department department);
    bool DepartmentExists(int? Id);
}
