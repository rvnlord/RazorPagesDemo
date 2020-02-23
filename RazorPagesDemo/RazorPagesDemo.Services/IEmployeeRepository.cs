using System.Collections.Generic;
using RazorPagesDemo.Models;

namespace RazorPagesDemo.Services
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int id);
        Employee Update(Employee updatedEmployee);
        Employee Add(Employee newEmployee);
        Employee Delete(int id);
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept);
        IEnumerable<Employee> Search(string searchTerm);
    }
}
