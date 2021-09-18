using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.Student;
using SchoolWiz.Common.Models.StudentGuardian;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Models.Student;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize]
    public class StudentController : ApplicationBaseController
    {
        private readonly ILogger _logger;
        private readonly IStudentService _studentService;
        private readonly IGuardianService _guardianService;

        public StudentController(UserManager<ApplicationUser> userManager, ILoggerFactory factory, IStudentService studentService, IGuardianService guardianService) 
            : base(userManager)
        {
            _logger = factory.CreateLogger("Student");
            _studentService = studentService;
            _guardianService = guardianService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var students = _studentService.GetAll(true).Select(s => new StudentIndexViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    MiddleName = s.MiddleName,
                    LastName = s.LastName,
                    IdentityNumber = s.IdentityNumber,
                    DateOfBirth = s.DateOfBirth, 
                    Age = s.Age,
                    MobileNumber = s.MobileNumber,
                    Email = s.Email,
                    InActive = s.IsDeleted
                }).OrderBy(s => s.Name);

                return View(students);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the student list.");
                _logger.LogError($@"An error occurred while trying to retrieve the student list - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var student = _studentService.GetById(id);
                if (student == null)
                    return NotFound();

                var guardians = _guardianService.GetStudentGuardians(id).Select(g => new StudentGuardianListViewModel
                {
                    Id = g.Guardian.Id, 
                    FirstName = g.Guardian.FirstName, 
                    MiddleName = g.Guardian.MiddleName, 
                    LastName = g.Guardian.LastName, 
                    GuardianType = g.Guardian.GuardianType.Name, 
                    MainContact = g.PrimaryContact,
                    InActive = g.Guardian.IsDeleted
                });

                return View(new StudentEditViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    IdentityNumber = student.IdentityNumber,
                    DateOfBirth = student.DateOfBirth.ToString("d"),
                    Age = student.Age,
                    MobileNumber = student.MobileNumber,
                    Email = student.Email,
                    MedicalConditions = student.MedicalConditions,
                    DoctorName = student.Doctor, 
                    DoctorContactNumber = student.DoctorPhoneNumber, 
                    UnderSupervisedMedication = student.UnderSupervisedMedication, 
                    MedicationCausesDrowsiness = student.MedicationCausesDrowsiness,
                    Guardians = guardians,
                    CreatedBy = GetUserName(student.CreatedById),
                    CreatedById = student.CreatedById,
                    CreatedDate = student.CreatedDate,
                    ModifiedBy = GetUserName(student.ModifiedById ?? Guid.Empty),
                    ModifiedById = student.ModifiedById,
                    ModifiedDate = student.ModifiedDate,
                    Inactive = student.IsDeleted
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the student.");
                _logger.LogError($@"An error occurred while trying to retrieve the student - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var student = _studentService.GetById(model.Id);
                    if (student == null)
                        return NotFound();

                    student.FirstName = model.FirstName;
                    student.MiddleName = model.MiddleName;
                    student.LastName = model.LastName;
                    student.IdentityNumber = model.IdentityNumber;
                    student.DateOfBirth = DateTime.Parse(model.DateOfBirth);
                    student.Age = model.Age;
                    student.MobileNumber = model.MobileNumber;
                    student.Email = model.Email;
                    student.Doctor = model.DoctorName;
                    student.DoctorPhoneNumber = model.DoctorContactNumber;
                    student.UnderSupervisedMedication = model.UnderSupervisedMedication;
                    student.MedicationCausesDrowsiness = model.MedicationCausesDrowsiness;
                    student.MedicalConditions = model.MedicalConditions;
                    student.IsDeleted = model.Inactive;
                    student.ModifiedById = model.ModifiedById;
                    student.ModifiedDate = model.ModifiedDate;

                    await _studentService.EditAsync(student);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Student successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the student.");
                _logger.LogError($@"An error occurred while trying to edit the student - {ex.Message}");
            }

            return View(model);
        }
    }
}