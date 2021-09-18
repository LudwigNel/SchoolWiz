using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.Address;
using SchoolWiz.Common.Models.Guardian;
using SchoolWiz.Common.Models.StudentRegistration;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Filters;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize]
    public class StudentRegistrationController : ApplicationBaseController
    {
        private readonly ILogger _logger;
        private readonly IStudentRegistrationService _studentRegistrationService;
        private readonly IGradeService _gradeService;
        private readonly IGuardianTypeService _guardianTypeService;
        private readonly ICityService _cityService;
        private readonly IAddressService _addressService;
        private readonly IAddressTypeService _addressTypeService;
        private readonly IAccountTypeService _accountTypeService;

        public StudentRegistrationController(ILoggerFactory factory, UserManager<ApplicationUser> userManager, IStudentRegistrationService studentRegistrationService, IGradeService gradeService, IGuardianTypeService guardianTypeService, ICityService cityService, IAddressService addressService, IAddressTypeService addressTypeService, IAccountTypeService accountTypeService)
        : base(userManager)
        {
            _logger = factory.CreateLogger("StudentRegistration");
            _studentRegistrationService = studentRegistrationService;
            _gradeService = gradeService;
            _guardianTypeService = guardianTypeService;
            _cityService = cityService;
            _addressService = addressService;
            _addressTypeService = addressTypeService;
            _accountTypeService = accountTypeService;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = string.IsNullOrEmpty(returnUrl) ? Url.ActionLink(nameof(Index), "Home") : returnUrl;

            var postalAddressType =
                _addressTypeService.GetByName(EnumExtensions.GetEnumDescription(AddressTypes.Postal));
            var physicalAddressType =
                _addressTypeService.GetByName(EnumExtensions.GetEnumDescription(AddressTypes.Physical));

            return View(new StudentRegistrationCreateViewModel
            {
                AccountTypes = new SelectList(_accountTypeService.GetAll(false), "Id", "Name"),
                SchoolYear = DateTime.Now.Year,
                Grades = new SelectList(_gradeService.GetAll(false), "Id", "Name"),
                MainGuardian = new GuardianCreateViewModel
                {
                    GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name"),
                    AddressDetail = new AddressEntryViewModel
                    {
                        ElementPrefix = "MainGuardian",
                        PostalAddress = new AddressCreateViewModel
                        {
                            Cities = new SelectList(_cityService.GetAll(false), "Id", "Name"),
                            AddressTypeId = postalAddressType.Id
                        },
                        PhysicalAddress = new AddressCreateViewModel
                        {
                            Cities = new SelectList(_cityService.GetAll(false), "Id", "Name"),
                            AddressTypeId = physicalAddressType.Id
                        }
                    }
                },
                AlternateGuardian = new AlternateGuardianCreateViewModel
                {
                    GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name"),
                    AddressDetail = new AddressEntryViewModel
                    {
                        ElementPrefix = "AlternateGuardian",
                        PostalAddress = new AddressCreateViewModel
                        {
                            Cities = new SelectList(_cityService.GetAll(false), "Id", "Name"),
                            AddressTypeId = postalAddressType.Id
                        },
                        PhysicalAddress = new AddressCreateViewModel
                        {
                            Cities = new SelectList(_cityService.GetAll(false), "Id", "Name"),
                            AddressTypeId = physicalAddressType.Id
                        }
                    }
                }
            });
        }

        [HttpPost]
        [ValidateAlternateGuardianFilter]
        public async Task<IActionResult> Register(StudentRegistrationCreateViewModel model)
        {
            try
            {
                TempData["DisplayToast"] = false;

                model.MainGuardian.Students = new[] { Guid.Empty };
                
                if (ModelState.IsValid)
                {
                    await _studentRegistrationService.Register(
                        model.Student.ConvertToStudent(model.CreatedById, model.CreatedDate),
                        model.MainGuardian.ConvertToGuardian(model.CreatedById, model.CreatedDate),
                        model.MainGuardian.AddressDetail.PostalAddress,
                        model.MainGuardian.AddressDetail.PhysicalAddress,
                        model.AlternateGuardian.HasValue()
                            ? model.AlternateGuardian.ConvertToGuardian(model.CreatedById, model.CreatedDate)
                            : null, model.AlternateGuardian.AddressDetail.PostalAddress,
                        model.AlternateGuardian.AddressDetail.PhysicalAddress, model.GradeId,
                        model.SchoolYear, model.AccountTypeId);

                    TempData["DisplayToast"] = true;
                    TempData["ToastType"] = EnumExtensions.GetEnumDescription(ToastType.Success).ToLower();
                    TempData["ToastTitle"] = "Success";
                    TempData["ToastMessage"] = "Student successfully registered";
                    return RedirectToAction(nameof(Index), model);
                }
            }
            catch (Exception ex)
            {
                TempData["DisplayToast"] = true;
                TempData["ToastType"] = EnumExtensions.GetEnumDescription(ToastType.Error).ToLower();
                TempData["ToastTitle"] = "Error";
                TempData["ToastMessage"] = "Something went wrong while trying to register the student.";
                _logger.LogError($@"An error occurred while trying to register student {model.Student.FirstName.Trim()} {model.Student.LastName.Trim()} - {ex.Message}");
            }
            model.Grades = new SelectList(_gradeService.GetAll(false), "Id", "Name");
            model.MainGuardian.GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name");
            model.MainGuardian.AddressDetail.PhysicalAddress.Cities = new SelectList(_cityService.GetAll(false), "Id", "Name");
            model.MainGuardian.AddressDetail.PostalAddress.Cities = new SelectList(_cityService.GetAll(false), "Id", "Name");
            model.AlternateGuardian.GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name");
            model.AlternateGuardian.AddressDetail.PhysicalAddress.Cities = new SelectList(_cityService.GetAll(false), "Id", "Name");
            model.AlternateGuardian.AddressDetail.PostalAddress.Cities = new SelectList(_cityService.GetAll(false), "Id", "Name");
            model.AccountTypes = new SelectList(_accountTypeService.GetAll(false), "Id", "Name");
            return View(nameof(Index), model);
        }
    }
}