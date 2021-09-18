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
using SchoolWiz.WebApp.Models.Vat;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VatController : ApplicationBaseController
    {
        private readonly IVatService _vatService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public VatController(IVatService vatService, UserManager<ApplicationUser> userManager, ILoggerFactory factory)
        : base(userManager)
        {
            _vatService = vatService;
            _userManager = userManager;
            _logger = factory.CreateLogger("Vat");
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var vat = _vatService.GetAll(true).Select(v =>
                    new VatIndexViewModel
                    {
                        Id = v.Id,
                        Value = v.Value,
                        ValidFrom = v.ValidFrom?.ToString("dd/MM/yyyy"), 
                        ValidTo = v.ValidTo?.ToString("dd/MM/yyyy"),
                        InActive = v.IsDeleted
                    });

                return View(vat);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the Vat list.");
                _logger.LogError($@"An error occurred while trying to retrieve the vat list - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VatCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VatCreateViewModel model)
        {
            try
            {
                var existing = _vatService.GetByValue(model.Value);
                if (existing != null)
                    ModelState.AddModelError(nameof(model.Value), "Vat already exists");

                if (ModelState.IsValid)
                {
                    var vat = new Vat
                    {
                        Value = model.Value,
                        ValidFrom = model.ValidFrom, 
                        ValidTo = model.ValidTo,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _vatService.CreateAsync(vat);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Errir",
                        "Vat successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the vat.");
                _logger.LogError($@"An error occurred while trying to create vat - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var vat = _vatService.GetById(id);
                if (vat == null)
                    return NotFound();

                return View(new VatEditViewModel
                {
                    Id = vat.Id,
                    Value = vat.Value,
                    ValidFrom = vat.ValidFrom, 
                    ValidTo = vat.ValidTo,
                    Inactive = vat.IsDeleted,
                    CreatedById = vat.CreatedById,
                    CreatedBy = GetUserName(vat.CreatedById),
                    CreatedDate = vat.CreatedDate,
                    ModifiedById = vat.ModifiedById,
                    ModifiedBy = GetUserName(vat.ModifiedById ?? Guid.Empty),
                    ModifiedDate = vat.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the vat.");
                _logger.LogError($@"An error occurred while trying to retrieve the vat for id (id) - {ex.Message}");
            }

            return View(new VatEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VatEditViewModel model)
        {
            try
            {
                var existing = _vatService.GetByValue(model.Value);
                if (existing != null && existing.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Value), "Vat already exists");

                if (ModelState.IsValid)
                {
                    var vat = _vatService.GetById(model.Id);
                    if (vat == null)
                        return NotFound();

                    vat.Value = model.Value;
                    vat.ValidFrom = model.ValidFrom;
                    vat.ValidTo = model.ValidTo;
                    vat.IsDeleted = model.Inactive;
                    vat.ModifiedById = model.ModifiedById;
                    vat.ModifiedDate = model.ModifiedDate;

                    await _vatService.EditAsync(vat);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Vat successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the Vat.");
                _logger.LogError($@"An error occurred while trying to edit the vat for id (id) - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var vat = _vatService.GetById(id);
                if (vat == null)
                    return NotFound();

                return PartialView("_Delete", new VatDeleteViewModel()
                {
                    Id = vat.Id,
                    Value = vat.Value
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the Vat.");
                _logger.LogError($@"An error occurred while trying to retrieve the vat for id (id) - {ex.Message}");
            }

            return PartialView(new VatDeleteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VatDeleteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vat = _vatService.GetById(model.Id);
                    if (vat == null)
                        return NotFound();

                    await _vatService.DeleteAsync(model.Id);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Vat successfully deleted.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the Vat.");
                _logger.LogError($@"An error occurred while trying to delete the vat for id (id) - {ex.Message}");
            }

            return View(model);
        }
    }
}