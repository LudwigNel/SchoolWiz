using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Models.City;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CityController : ApplicationBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;

        public CityController(UserManager<ApplicationUser> userManager, ICityService cityService, ICountryService countryService, IProvinceService provinceService, ILoggerFactory factory) 
            : base(userManager)
        {
            _userManager = userManager;
            _logger = factory.CreateLogger("City");
            _cityService = cityService;
            _countryService = countryService;
            _provinceService = provinceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var cities = _cityService.GetAll(true).Select(c => new CityIndexViewModel
                {
                    Id = c.Id, 
                    Name = c.Name, 
                    Province = c.Province.Name, 
                    Country =  c.Province.Country.Name,
                    InActive = c.IsDeleted, 
                });

                return View(cities);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the city list.");
                _logger.LogError($@"An error occurred while trying to retrieve cities - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var model = new CityCreateViewModel
                {
                    Countries = new SelectList(_countryService.GetAll(false), "Id", "Name")
                };

                return View(model);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to load the create city page.");
                _logger.LogError($@"An error occurred while trying to load the Create City view - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityCreateViewModel model)
        {
            try
            {
                var existing = _cityService.GetByName(model.Name, model.ProvinceId);
                if (existing != null)
                {
                    ModelState.AddModelError(nameof(model.Name), "City already exists.");
                    model.Countries = new SelectList(_countryService.GetAll(false), "Id", "Name");
                }

                if (ModelState.IsValid)
                {
                    var city = new City
                    {
                        Name = model.Name, 
                        ProvinceId = model.ProvinceId, 
                        IsDeleted = model.InActive, 
                        CreatedById = model.CreatedById, 
                        CreatedDate = model.CreatedDate
                    };
                    await _cityService.CreateAsync(city);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "City successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the city.");
                _logger.LogError($@"An error occurred while trying to create the City - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var city = _cityService.GetById(id);
                if (city == null)
                    return NotFound();

                return View(new CityEditViewModel
                {
                    Id = city.Id,
                    Name = city.Name,
                    ProvinceId = city.ProvinceId,
                    CountryId = city.Province.CountryId,
                    Countries = new SelectList(_countryService.GetAll(false), "Id", "Name"),
                    InActive = city.IsDeleted,
                    CreatedById = city.CreatedById,
                    CreatedBy = GetUserName(city.CreatedById),
                    CreatedDate = city.CreatedDate,
                    ModifiedById = city.ModifiedById,
                    ModifiedBy = GetUserName(city.ModifiedById ?? Guid.Empty),
                    ModifiedDate = city.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the city.");
                _logger.LogError($@"An error occurred while trying to retrieve the City with id {id} - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityEditViewModel model)
        {
            try
            {
                var existing = _cityService.GetByName(model.Name, model.ProvinceId);
                if (existing != null && existing.Id != model.Id)
                {
                    ModelState.AddModelError(nameof(model.Name), "City already exists.");
                    model.Countries = new SelectList(_countryService.GetAll(false), "Id", "Name");
                }

                if (ModelState.IsValid)
                {
                    var city = _cityService.GetById(model.Id);
                    if (city == null)
                        return NotFound();
                    
                    city.Name = model.Name;
                    city.ProvinceId = model.ProvinceId;
                    city.IsDeleted = model.InActive;
                    city.ModifiedById = model.ModifiedById;
                    city.ModifiedDate = model.ModifiedDate;

                    await _cityService.EditAsync(city);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "City successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the city.");
                _logger.LogError($@"An error occurred while trying to edit City with id {model.Id} - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var city = _cityService.GetById(id);
                if (city == null)
                    return NotFound();

                return PartialView("_Delete", new CityDeleteViewModel
                {
                    Id = city.Id,
                    Name = city.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the city.");
                _logger.LogError($@"An error occurred while trying to retrieve City with id {id} - {ex.Message}");
            }

            return PartialView("_Delete", new CityDeleteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CityDeleteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var city = _cityService.GetById(model.Id);
                    if (city == null)
                        return NotFound();

                    await _cityService.DeleteAsync(model.Id);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "City successfully deleted.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the city.");
                _logger.LogError($@"An error occurred while trying to delete City with id {model.Id} - {ex.Message}");
            }

            return PartialView("_Delete", new CityDeleteViewModel());
        }

        [HttpGet]
        //Used for lookups
        public JsonResult GetCity(Guid cityId)
        {
            try
            {
                var city = _cityService.GetById(cityId);

                return Json(new { province = city?.Province?.Name, country = city?.Province?.Country?.Name});
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to retrieve city for id id {cityId} - {ex.Message}");
            }

            return null;
        }
    }
}