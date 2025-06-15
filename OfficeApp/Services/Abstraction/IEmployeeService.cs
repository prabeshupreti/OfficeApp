using OfficeApp.Models;

namespace OfficeApp.Services.Abstraction;

public interface IEmployeeService
{
    void AddEmployee(Employee employee);
    void DeleteEmployee(Employee employee);
    List<Employee> GetEmployees();
    IQueryable<Employee> GetAllEmployees();
    Employee GetEmployeeById(int? Id);
    void UpdateEmployee(Employee employee);
    bool EmployeeExists(int? Id);
}
