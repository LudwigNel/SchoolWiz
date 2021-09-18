using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;
using SchoolWiz.Services;
using SchoolWiz.Web.Models.Country;

namespace SchoolWiz.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CountryController : ApplicationBaseController
    {
        private readonly ICountryService _countryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, ApplicationDbContext context, ILogger<CountryController> logger, UserManager<ApplicationUser> userManager) 
            : base(userManager)
        {
            _countryService = countryService;
            _logger = logger;
            _userManager = userManager;
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
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
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
                if (ModelState.IsValid)
                {
                    var country = _countryService.GetById(model.Id);
                    country.Name = model.Name;
                    country.IsDeleted = model.Inactive;
                    country.ModifiedById = model.ModifiedById;
                    country.ModifiedDate = model.ModifiedDate;

                    await _countryService.EditAsync(country);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
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

                return View(new CountryDeleteViewModel
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to retrieve country {id}. {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CountryDeleteViewModel model)
        {
            try
            {
                await _countryService.DeleteAsync(model.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to delete country {model.Id}. {ex.Message}");
            }

            return View();
        }
    }
}