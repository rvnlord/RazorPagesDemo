using System;
using System.Collections.Generic;
using System.Linq;
using RazorPagesDemo.Models;

namespace RazorPagesDemo.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
                new Employee { Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@pragimtech.com", PhotoPath = "mary.jpg" },
                new Employee { Id = 2, Name = "John", Department = Dept.IT, Email = "john@pragimtech.com", PhotoPath = "john.png" },
                new Employee { Id = 3, Name = "Sara", Department = Dept.IT, Email = "sara@pragimtech.com", PhotoPath = "sara.jpg" },
                new Employee { Id = 4, Name = "David", Department = Dept.Payroll, Email = "david@pragimtech.com" }
            };
        }

        public IEnumerable<Employee> GetAllEmployees() => _employeeList;
        public Employee GetEmployeeById(int id) => _employeeList.FirstOrDefault(e => e.Id == id);

        public Employee Update(Employee updatedEmployee)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == updatedEmployee.Id);

            if (employee == null) 
                return null;

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Department = updatedEmployee.Department;
            employee.PhotoPath = updatedEmployee.PhotoPath;

            return employee;
        } 

        public Employee Add(Employee newEmployee)
        {
            newEmployee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(newEmployee);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _employeeList.FirstOrDefault(e => e.Id == id);

            if (employeeToDelete != null)
                _employeeList.Remove(employeeToDelete);

            return employeeToDelete;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _employeeList;

            if (dept.HasValue)
                query = query.Where(e => e.Department == dept.Value);

            return query.GroupBy(e => e.Department)
                .Select(g => new DeptHeadCount
                {
                    Department = g.Key ?? new Dept(),
                    Count = g.Count()
                }).ToList();
        }

        public IEnumerable<Employee> Search(string searchTerm) =>
            string.IsNullOrWhiteSpace(searchTerm) 
                ? _employeeList 
                : _employeeList.Where(e => e.Name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase) 
                    || e.Email.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
    }
}