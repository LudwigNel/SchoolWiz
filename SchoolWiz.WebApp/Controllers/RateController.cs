using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.Rate;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Models.Rate;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator, Billing")]
    public class RateController : ApplicationBaseController
    {
        private readonly ILogger _logger;
        private readonly IRateService _rateService;

        public RateController(UserManager<ApplicationUser> userManager, IRateService rateService, ILoggerFactory loggerFactory) : base(userManager)
        {
            _logger = loggerFactory.CreateLogger("Rate");
            _rateService = rateService;
        }

        public IActionResult Index()
        {
            try
            {
                var rates = _rateService.GetAll(true).Select(r => new RateIndexViewModel
                {
                    Id = r.Id,
                    Description = r.Description,
                    ValidFrom = r.ValidFrom?.ToString("dd/MM/yyyy"),
                    ValidTo = r.ValidTo?.ToString("dd/MM/yyyy"),
                    Value = r.Value.ToString("C"),
                    InActive = r.IsDeleted
                });

                return View(rates);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the rate list.");
                _logger.LogError($@"An error occurred while trying to retrieve the rate list. {ex.Message}");
            }

            return View(new List<RateIndexViewModel>());
        }

        [HttpGet]
        public IActionResult Create(string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                return View(new RateCreateViewModel());
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve load the rate create view.");
                _logger.LogError($@"An error occurred while trying to load rate create view. {ex.Message}");
            }

            return View(new RateCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RateCreateViewModel model)
        {
            try
            {
                var existingRate = _rateService.GetRate(model.Description, model.ValidFrom, model.ValidTo);
                if(existingRate != null)
                    ModelState.AddModelError("Description", "Rate already exists");

                if (ModelState.IsValid)
                {
                    await _rateService.CreateAsync(model).ConfigureAwait(false);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success", "Rate Successfully Created");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve create the new rate.");
                _logger.LogError($@"An error occurred while trying to create the new rate. {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                var rate = _rateService.GetById(id);

                if (rate == null)
                    return NotFound();

                return View(new RateEditViewModel
                {
                    Id = rate.Id,
                    Description = rate.Description,
                    ValidFrom = rate.ValidFrom,
                    ValidTo = rate.ValidTo,
                    Value = rate.Value,
                    Inactive = rate.IsDeleted,
                    CreatedById = rate.CreatedById,
                    CreatedBy = GetUserName(rate.CreatedById),
                    CreatedDate = rate.CreatedDate,
                    ModifiedById = rate.ModifiedById,
                    ModifiedBy = GetUserName(rate.ModifiedById ?? Guid.Empty),
                    ModifiedDate = rate.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the rate.");
                _logger.LogError($@"An error occurred while trying to retrieve the rate. {ex.Message}");
            }

            return View(new RateEditViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RateEditViewModel model)
        {
            try
            {
                var existingRate = _rateService.GetRate(model.Description, model.ValidFrom, model.ValidTo);
                if(existingRate != null && existingRate.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Description), "Rate already exists");

                if (ModelState.IsValid)
                {
                    await _rateService.EditAsync(model).ConfigureAwait(false);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success", "Rate Successfully Updated");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve edit the new rate.");
                _logger.LogError($@"An error occurred while trying to edit the new rate. {ex.Message}");
            }

            return View(model);
        }
    }
}
