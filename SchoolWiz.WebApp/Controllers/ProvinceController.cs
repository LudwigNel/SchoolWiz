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
using SchoolWiz.WebApp.Models.Province;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProvinceController : ApplicationBaseController
    {
        private readonly IProvinceService _provinceService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ICountryService _countryService;

        public ProvinceController(IProvinceService provinceService, UserManager<ApplicationUser> userManager, ILoggerFactory factory, ICountryService countryService)
            : base(userManager)
        {
            _provinceService = provinceService;
            _userManager = userManager;
            _logger = factory.CreateLogger("Province");
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var provinces = _provinceService.GetAll(true).Select(p => new ProvinceIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    InActive = p.IsDeleted
                });

                return View(provinces);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the province list.");
                _logger.LogError($@"An error occurred while trying to retrieve the provinces - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProvinceCreateViewModel { Countries = new SelectList(_countryService.GetAll(false), "Id", "Name") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProvinceCreateViewModel model)
        {
            try
            {
                var existing = _provinceService.GetByName(model.Name, model.CountryId);
                if (existing != null)
                {
                    ModelState.AddModelError(nameof(model.Name), "Province already exists.");
                    model.Countries = new SelectList(_countryService.GetAll(false), "Id", "Name");
                }

                if (ModelState.IsValid)
                {
                    var province = new Province
                    {
                        Name = model.Name,
                        CountryId = model.CountryId,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _provinceService.Create(province);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Province successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the province.");
                _logger.LogError($@"An error occurred while trying to create a new provinces - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var province = _provinceService.GetById(id);
                if (province == null)
                    return NotFound();

                return View(new ProvinceEditViewModel
                {
                    Id = province.Id,
                    Name = province.Name,
                    Countries = new SelectList(_countryService.GetAll(false), "Id", "Name"),
                    CountryId = province.CountryId,
                    Inactive = province.IsDeleted,
                    CreatedById = province.CreatedById,
                    CreatedBy = GetUserName(province.CreatedById),
                    CreatedDate = province.CreatedDate,
                    ModifiedById = province.ModifiedById,
                    ModifiedBy = GetUserName(province.ModifiedById ?? Guid.Empty),
                    ModifiedDate = province.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the province.");
                _logger.LogError($@"An error occurred while trying to retrieve the provinces - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProvinceEditViewModel model)
        {
            try
            {
                var existing = _provinceService.GetByName(model.Name, model.CountryId);
                if (existing != null && existing.Id != model.Id)
                {
                    ModelState.AddModelError(nameof(model.Name), "Province already exists.");
                    model.Countries = new SelectList(_countryService.GetAll(false), "Id", "Name");
                }

                if (ModelState.IsValid)
                {
                    var province = _provinceService.GetById(model.Id);
                    if (province == null)
                        return NotFound();

                    province.Name = model.Name;
                    province.CountryId = model.CountryId;
                    province.IsDeleted = model.Inactive;
                    province.ModifiedById = model.ModifiedById;
                    province.ModifiedDate = model.ModifiedDate;

                    await _provinceService.Edit(province);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Province successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the province.");
                _logger.LogError($@"An error occurred while trying to edit the provinces with id {model.Id} - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var province = _provinceService.GetById(id);
                if (province == null)
                    return NotFound();

                return PartialView("_Delete", new ProvinceDeleteViewModel
                {
                    Id = province.Id,
                    Name = province.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the province.");
                _logger.LogError($@"An error occurred while trying to retrieve the provinces with id {id} - {ex.Message}");
            }

            return PartialView("_Delete", new ProvinceDeleteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProvinceDeleteViewModel model)
        {
            try
            {
                await _provinceService.Delete(model.Id);

                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                    "Success", "Province successfully deleted.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the province.");
                _logger.LogError($@"An error occurred while trying to delete the provinces with id {model.Id} - {ex.Message}");
            }

            return RedirectToAction(nameof(Delete), new {id = model.Id});
        }

        [HttpGet]
        public JsonResult GetProvincesForCountry(Guid countryId)
        {
            try
            {
                var provinces = _provinceService.GetProvincesForCountry(countryId);

                return Json(new SelectList(provinces, "Id", "Name"));
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to retrieve provinces for country id {countryId} - {ex.Message}");
            }

            return null;
        }
    }
}