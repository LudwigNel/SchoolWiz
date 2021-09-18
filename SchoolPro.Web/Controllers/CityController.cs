using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;
using SchoolWiz.Services;
using SchoolWiz.Web.Models.City;

namespace SchoolWiz.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CityController : ApplicationBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CityController> _logger;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;

        public CityController(UserManager<ApplicationUser> userManager, ILogger<CityController> logger, ICityService cityService, ICountryService countryService, IProvinceService provinceService) 
            : base(userManager)
        {
            _userManager = userManager;
            _logger = logger;
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
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to create the City - {ex.Message}");
            }

            return View();
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to edit City with id {model.Id} - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var city = _cityService.GetById(id);
                if (city == null)
                    return NotFound();

                return View(new CityDeleteViewModel
                {
                    Id = city.Id,
                    Name = city.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to retrieve City with id {id} - {ex.Message}");
            }

            return View();
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to delet City with id {model.Id} - {ex.Message}");
            }

            return View();
        }
    }
}