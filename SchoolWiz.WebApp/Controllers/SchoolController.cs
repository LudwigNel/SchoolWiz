using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Models.School;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SchoolController : ApplicationBaseController
    {
        private readonly ISchoolService _schoolService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public SchoolController(ISchoolService schoolService, UserManager<ApplicationUser> userManager, ILoggerFactory factory)
        : base(userManager)
        {
            _schoolService = schoolService;
            _userManager = userManager;
            _logger = factory.CreateLogger("School");
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var school = _schoolService.GetSchool();
                if (school == null)
                    return View(new SchoolIndexViewModel());

                return View(new SchoolIndexViewModel
                {
                    Id = school.Id,
                    Name = school.Name,
                    RegistrationNo = school.RegistrationNo,
                    VatNo = school.VatNo,
                    ContactPerson = school.ContactPerson,
                    PhoneNumber = school.PhoneNumber,
                    Email = school.Email,
                    ImageUrl = school.ImageUrl,
                    IsDeleted = school.IsDeleted,
                    CreatedById = school.CreatedById,
                    CreatedBy = GetUserName(school.CreatedById),
                    CreatedDate = school.CreatedDate,
                    ModifiedById = school.ModifiedById,
                    ModifiedBy = GetUserName(school.ModifiedById ?? Guid.Empty),
                    ModifiedDate = school.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the school information.");
                _logger.LogError($@"An error occurred while trying to display school details - {ex.Message}");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var school = _schoolService.GetSchool();
                    if (school == null)
                    {
                        school = new School
                        {
                            Name = model.Name,
                            VatNo = model.VatNo,
                            RegistrationNo = model.RegistrationNo,
                            ContactPerson = model.ContactPerson,
                            PhoneNumber = model.PhoneNumber,
                            Email = model.Email,
                            IsDeleted = model.IsDeleted,
                            CreatedById = model.CreatedById,
                            CreatedDate = model.CreatedDate
                        };

                        if (model.ImageUrl != null)
                            ConvertImageToBytes(model, school);

                        await _schoolService.CreateSchoolAsync(school);

                        SetTempDataForToastNotification(true,
                            EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                            "School information successfully created.");
                    }
                    else
                    {
                        school.Name = model.Name;
                        school.VatNo = model.VatNo;
                        school.RegistrationNo = model.RegistrationNo;
                        school.ContactPerson = model.ContactPerson;
                        school.PhoneNumber = model.PhoneNumber;
                        school.Email = model.Email;
                        school.IsDeleted = model.IsDeleted;
                        school.ModifiedById = model.CreatedById;
                        school.ModifiedDate = model.CreatedDate;

                        if (model.ImageUrl != null)
                            ConvertImageToBytes(model, school);

                        await _schoolService.EditSchool(school);

                        SetTempDataForToastNotification(true,
                            EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                            "School Information successfully updated.");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to save the school information.");
                _logger.LogError($@"An error occurred while trying to add or update school - {ex.Message}");
            }

            return View(nameof(Index));
        }

        private static void ConvertImageToBytes(SchoolCreateViewModel model, School school)
        {
            byte[] imageBytes = null;
            using (var fs1 = model.ImageUrl.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                imageBytes = ms1.ToArray();
            }

            school.ImageUrl = imageBytes;
        }
    }
}