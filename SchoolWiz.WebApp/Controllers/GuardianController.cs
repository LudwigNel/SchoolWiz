using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.Address;
using SchoolWiz.Common.Models.Guardian;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Models.Guardian;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize]
    public class GuardianController : ApplicationBaseController
    {
        private readonly ILogger _logger;
        private readonly IGuardianService _guardianService;
        private readonly IGuardianTypeService _guardianTypeService;
        private readonly ICityService _cityService;
        private readonly IAddressService _addressService;
        private readonly IAddressTypeService _addressTypeService;
        private readonly IStudentService _studentService;
        private readonly IStudentGuardianService _studentGuardianService;

        public GuardianController(UserManager<ApplicationUser> userManager, IGuardianService guardianService, ILoggerFactory factory, IGuardianTypeService guardianTypeService, ICityService cityService, IAddressService addressService, IAddressTypeService addressTypeService, IStudentService studentService, IStudentGuardianService studentGuardianService)
            : base(userManager)
        {
            _guardianService = guardianService;
            _logger = factory.CreateLogger("Guardian");
            _guardianTypeService = guardianTypeService;
            _cityService = cityService;
            _addressService = addressService;
            _addressTypeService = addressTypeService;
            _studentService = studentService;
            _studentGuardianService = studentGuardianService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var guardians = _guardianService.GetAll(true).Select(g => new GuardianIndexViewModel
                {
                    Id = g.Id,
                    FirstName = g.FirstName,
                    MiddleName = g.MiddleName,
                    LastName = g.LastName,
                    MobileNumber = g.MobileNumber,
                    Email = g.Email,
                    IdentityNumber = g.IdentityNumber,
                    InActive = g.IsDeleted
                });

                return View(guardians);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the guardian list.");
                _logger.LogError($@"An error occurred while trying to retrieve the guardian list - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create(string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                var model = CreateGuardianCreateModel();

                return View(model);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to load the create guardian page.");
                _logger.LogError($@"An error occurred while trying to load the create guardian page - {ex.Message}");
            }

            return View(new GuardianCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuardianCreateViewModel model)
        {
            try
            {
                var existingGuardian = _guardianService.GetByIdentityNumber(model.IdentityNumber);
                if (existingGuardian != null)
                {
                    ModelState.AddModelError("IdentityNumber", "Guardian with same Identity Number already exists.");
                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Warning).ToLower(),
                        "Error", "There is already a Guardian with the same Identity Number.");
                    model.GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    await _guardianService.CreateAsync(model);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "Guardian successfully created");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the guardian.");
                _logger.LogError($@"An error occurred while trying to create the guardian - {ex.Message}");
            }

            model.GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                var guardian = _guardianService.GetById(id);
                if (guardian == null)
                    return NotFound();

                var model = new GuardianEditViewmodel
                {
                    Id = guardian.Id,
                    FirstName = guardian.FirstName,
                    MiddleName = guardian.MiddleName,
                    LastName = guardian.LastName,
                    IdentityNumber = guardian.IdentityNumber,
                    HomePhoneNumber = guardian.HomePhoneNumber,
                    WorkPhoneNumber = guardian.WorkPhoneNumber,
                    MobileNumber = guardian.MobileNumber,
                    Email = guardian.Email,
                    GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name"),
                    GuardianTypeId = guardian.GuardianTypeId,
                    Inactive = guardian.IsDeleted,
                    CreatedBy = GetUserName(guardian.CreatedById),
                    CreatedById = guardian.CreatedById,
                    CreatedDate = guardian.CreatedDate,
                    ModifiedBy = GetUserName(guardian.ModifiedById ?? Guid.Empty),
                    ModifiedById = guardian.ModifiedById,
                    ModifiedDate = guardian.ModifiedDate,
                    PostalAddress = { Cities = new SelectList(_cityService.GetAll(false), "Id", "Name") },
                    PhysicalAddress = { Cities = new SelectList(_cityService.GetAll(false), "Id", "Name") },
                    StudentList = new SelectList(_studentService.GetAllLookup(), "Id", "Name")
                };

                var postalAddressType =
                    _addressTypeService.GetByName(EnumExtensions.GetEnumDescription(AddressTypes.Postal));
                var physicalAddressType =
                    _addressTypeService.GetByName(EnumExtensions.GetEnumDescription(AddressTypes.Physical));
                if (guardian.GuardianAddresses.Any(ga => ga.Address.AddressTypeId == physicalAddressType.Id))
                {
                    var physicalAddress = guardian.GuardianAddresses.FirstOrDefault(ga =>
                        ga.Address.AddressType.Name == EnumExtensions.GetEnumDescription(AddressTypes.Physical) && !ga.IsDeleted && !ga.Address.IsDeleted);
                    if (physicalAddress != null)
                    {
                        model.PhysicalAddress.Id = physicalAddress.Address.Id;
                        model.PhysicalAddress.UnitNumber = physicalAddress.Address.UnitNumber;
                        model.PhysicalAddress.ComplexName = physicalAddress.Address.ComplexName;
                        model.PhysicalAddress.StreetAddress = physicalAddress.Address.StreetAddress;
                        model.PhysicalAddress.Suburb = physicalAddress.Address.Suburb;
                        model.PhysicalAddress.CityId = physicalAddress.Address.CityId;

                        var physicalCity = _cityService.GetById(physicalAddress.Address.CityId);
                        if (physicalCity != null)
                        {
                            model.PhysicalAddress.Province = physicalCity.Province.Name;
                            model.PhysicalAddress.Country = physicalCity.Province.Country.Name;
                        }

                        model.PhysicalAddress.PostalCode = physicalAddress.Address.PostalCode;
                    }
                }

                if (guardian.GuardianAddresses.Any(ga => ga.Address.AddressTypeId == postalAddressType.Id))
                {
                    var postalAddress = guardian.GuardianAddresses.FirstOrDefault(ga =>
                        ga.Address.AddressType.Name == EnumExtensions.GetEnumDescription(AddressTypes.Postal) && !ga.IsDeleted && !ga.Address.IsDeleted);
                    if (postalAddress != null)
                    {
                        model.PostalAddress.Id = postalAddress.Address.Id;
                        model.PostalAddress.UnitNumber = postalAddress.Address.UnitNumber;
                        model.PostalAddress.ComplexName = postalAddress.Address.ComplexName;
                        model.PostalAddress.StreetAddress = postalAddress.Address.StreetAddress;
                        model.PostalAddress.Suburb = postalAddress.Address.Suburb;
                        model.PostalAddress.CityId = postalAddress.Address.CityId;

                        var postalCity = _cityService.GetById(postalAddress.Address.CityId);
                        if (postalCity != null)
                        {
                            model.PostalAddress.Province = postalCity.Province.Name;
                            model.PostalAddress.Country = postalCity.Province.Country.Name;
                        }

                        model.PostalAddress.PostalCode = postalAddress.Address.PostalCode;
                    }

                    if (guardian.StudentGuardians != null && guardian.StudentGuardians.Any())
                    {
                        model.MainGuardian =
                            guardian.StudentGuardians.Any(g => g.GuardianId == guardian.Id && g.PrimaryContact);
                        model.Students = guardian.StudentGuardians.Select(sg => sg.StudentId).ToArray();
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the guardian.");
                _logger.LogError($@"An error occurred while trying to retrieve the guardian - {ex.Message}");
            }

            return View(new GuardianEditViewmodel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GuardianEditViewmodel model, string returnUrl)
        {
            try
            {
                TempData["DisplayToast"] = false;

                var existingGuardian = _guardianService.GetByIdentityNumber(model.IdentityNumber);
                if (existingGuardian != null && existingGuardian.Id != model.Id)
                {
                    ModelState.AddModelError("IdentityNumber", "Guardian with same Identity Number already exists.");
                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Warning).ToLower(),
                        "Error", "There is already a Guardian with the same Identity Number.");
                    model.GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    await _guardianService.EditAsync(model);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "Guardian successfully updated");

                    if (!string.IsNullOrEmpty(returnUrl) && returnUrl.Contains("Student/Edit"))
                        return Redirect(returnUrl);
                    return RedirectToAction(nameof(Index), nameof(Guardian));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the guardian.");
                _logger.LogError($@"An error occurred while trying to edit the guardian - {ex.Message}");
            }

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        private GuardianCreateViewModel CreateGuardianCreateModel()
        {
            var model = new GuardianCreateViewModel
            {
                GuardianTypes = new SelectList(_guardianTypeService.GetAll(false), "Id", "Name"),
                AddressDetail = new AddressEntryViewModel
                {
                    PostalAddress = new AddressCreateViewModel
                    {
                        AddressTypes = new SelectList(_addressTypeService.GetAll(false), "Id", "Name"),
                        Cities = new SelectList(_cityService.GetAll(false), "Id", "Name")
                    },
                    PhysicalAddress = new AddressCreateViewModel
                    {
                        AddressTypes = new SelectList(_addressTypeService.GetAll(false), "Id", "Name"),
                        Cities = new SelectList(_cityService.GetAll(false), "Id", "Name")
                    }
                },
                StudentList = new SelectList(_studentService.GetAllLookup(), "Id", "Name")
            };
            return model;
        }
    }
}