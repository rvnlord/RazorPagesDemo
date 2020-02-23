using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RazorPagesDemo.Models;

namespace RazorPagesDemo.Services
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public SqlEmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public Employee Add(Employee employeeToadd)
        {
            //_db.Employees.Add(employeeToadd);
            //_db.SaveChanges();
            //return employeeToadd;

            _db.Database.ExecuteSqlRaw("spInsertEmployee {0}, {1}, {2}, {3}",
                employeeToadd.Name,
                employeeToadd.Email,
                employeeToadd.PhotoPath,
                employeeToadd.Department);
            return employeeToadd;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _db.Employees.Find(id);
            if (employeeToDelete == null)
                return null;

            _db.Employees.Remove(employeeToDelete);
            _db.SaveChanges();
            return employeeToDelete;
        }

        public Employee GetEmployeeById(int id)
        {
            //return _db.Employees.Find(id);
            // - OR
            //return _db.Employees.FromSqlRaw("spGetEmployeeById {0}", id).ToList().FirstOrDefault();
            // - OR

            var paramId = new SqlParameter("@Id", id);
            return _db.Employees.FromSqlRaw("spGetEmployeeById @Id", paramId).ToList().FirstOrDefault();
        }

        public Employee Update(Employee employeeToUpdate)
        {
            var employee = _db.Employees.Attach(employeeToUpdate);
            employee.State = EntityState.Modified;
            _db.SaveChanges();
            return employeeToUpdate;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            //return _db.Employees;
            return _db.Employees.FromSqlRaw("SELECT * FROM Employees").ToList();
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            var depts = _db.Employees.Select(e => e.Department).Where(d => d != null).Distinct().Cast<Dept>();
            return depts.Select(d => new DeptHeadCount
            {
                Department = d,
                Count = _db.Employees.Count(e => e.Department == d)
            });
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _db.Employees;

            searchTerm = searchTerm.ToLowerInvariant();
            return _db.Employees.Where(e => e.Name.ToLower().Contains(searchTerm) || e.Email.ToLower().Contains(searchTerm));
        }
    }
}
