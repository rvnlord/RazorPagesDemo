using Microsoft.AspNetCore.Mvc;
using RazorPagesDemo.Models;
using RazorPagesDemo.Services;

namespace RazorPagesDemo.Pages.Shared.Components.HeadCount
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IViewComponentResult Invoke(Dept? department = null)
        {
            var result = _employeeRepository.EmployeeCountByDept(department);
            return View(result);
        }
    }
}
