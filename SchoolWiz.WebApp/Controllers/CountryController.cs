using System;
using System.Linq;
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
using SchoolWiz.WebApp.Models.Country;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CountryController : ApplicationBaseController
    {
        private readonly ICountryService _countryService;
        private readonly ILogger _logger;

        public CountryController(ICountryService countryService, ILoggerFactory factory, UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            _countryService = countryService;
            _logger = factory.CreateLogger("Country");
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var countries = _countryService.GetAll(true).Select(c => new CountryIndexViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    InActive = c.IsDeleted
                });
                return View(countries);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the country list.");
                _logger.LogError($@"An error occurred while trying to load the country list. {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CountryCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryCreateViewModel model)
        {
            try
            {
                var existing = _countryService.GetByName(model.Name);
                if (existing != null)
                    ModelState.AddModelError(nameof(model.Name), "Country already exists");

                if (ModelState.IsValid)
                {
                    var country = new Country
                    {
                        Name = model.Name,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _countryService.CreateAsync(country);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Country successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the country.");
                _logger.LogError($@"An error occurred while trying to create country {model.Name} - {ex.Message}");

            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var country = _countryService.GetById(id);
                if (country == null)
                    return NotFound();

                return View(new CountryEditViewModel
                {
                    Id = country.Id,
                    Name = country.Name,
                    Inactive = country.IsDeleted,
                    CreatedById = country.CreatedById,
                    CreatedBy = GetUserName(country.CreatedById),
                    CreatedDate = country.CreatedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the country.");
                _logger.LogError($@"An error occurred while trying to display details . {ex.Message}");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CountryEditViewModel model)
        {
            try
            {
                var existing = _countryService.GetByName(model.Name);
                if (existing != null && existing.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Name), "Country already exists");

                if (ModelState.IsValid)
                {
                    var country = _countryService.GetById(model.Id);

                    if (country == null)
                        return NotFound();

                    country.Name = model.Name;
                    country.IsDeleted = model.Inactive;
                    country.ModifiedById = model.ModifiedById;
                    country.ModifiedDate = model.ModifiedDate;

                    await _countryService.EditAsync(country);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Country successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the country.");
                _logger.LogError($@"An error occurred while trying to edit country {model.Id}. {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var country = _countryService.GetById(id);
                if (country == null)
                    return NotFound();

                return PartialView("_Delete", new CountryDeleteViewModel
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the country.");
                _logger.LogError($@"An error occurred while trying to retrieve country {id}. {ex.Message}");
            }

            return PartialView("_Delete", new CountryDeleteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CountryDeleteViewModel model)
        {
            try
            {
                await _countryService.DeleteAsync(model.Id);

                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                    "Success", "Country successfully deleted.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the country.");
                _logger.LogError($@"An error occurred while trying to delete country {model.Id}. {ex.Message}");
            }

            return RedirectToAction(nameof(Delete), new { id = model.Id });
        }
    }
}