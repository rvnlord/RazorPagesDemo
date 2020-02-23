using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models;
using RazorPagesDemo.Services;

namespace RazorPagesDemo.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public bool Notify { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _env = env;
        }

        public IActionResult OnGet(int? id)
        {
            Employee = id.HasValue ? _employeeRepository.GetEmployeeById(id.Value) : new Employee();

            if (Employee == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Photo != null) // requires `enctype="multipart/form-data"` set for the 'form' where the upload control is defined, otherwise it will always be null
            {
                if (Employee.PhotoPath != null)
                {
                    var filePath = Path.Combine(_env.WebRootPath, "images", Employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                Employee.PhotoPath = ProcessUploadedFile();
            }

            Employee = Employee.Id > 0 ? _employeeRepository.Update(Employee) : _employeeRepository.Add(Employee);
            return RedirectToPage("Index");
        } 

        public IActionResult OnPostUpdateNotificationPreferences(int id) // You need to have asp-page-handler="<Method Name w/o OnPost>" and OnPost<Method Name w/o OnPost> in '.cs' file, it won't  bind properly otherwise. You can't skip 'OnPost' in '.cs' file and you can't include 'OnPost' in 'asp-page-handler'.
        {
            Message = Notify ? "Thank you for turning on notifications" : "You have turned off email notifications";
            TempData["message"] = Message;
            return RedirectToPage("Details", new { id });
        }

        private string ProcessUploadedFile()
        {
            if (Photo == null) 
                return null;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            var uniqueFileName = Guid.NewGuid() + "_" + Photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            Photo.CopyTo(fileStream);

            return uniqueFileName;
        }
    }
}