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
using SchoolWiz.WebApp.Models.GuardianType;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GuardianTypeController : ApplicationBaseController
    {
        private readonly IGuardianTypeService _guardianTypeService;
        private readonly ILogger _logger;

        public GuardianTypeController(IGuardianTypeService guardianTypeService, UserManager<ApplicationUser> userManager, ILoggerFactory factory)
        : base(userManager)
        {
            _guardianTypeService = guardianTypeService;
            _logger = factory.CreateLogger("GuardianType");
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var guardianTypeList = _guardianTypeService.GetAll(true).Select(status =>
                    new GuardianTypeIndexViewModel
                    {
                        Id = status.Id,
                        Name = status.Name,
                        InActive = status.IsDeleted
                    });

                return View(guardianTypeList);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the guardian type list.");
                _logger.LogError($@"An error occurred while trying to retrieve the guardian type list - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new GuardianTypeCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuardianTypeCreateViewModel model)
        {
            try
            {
                var existing = _guardianTypeService.GetByName(model.Name);
                if (existing != null)
                    ModelState.AddModelError(nameof(model.Name), "Guardian type already exists");

                if (ModelState.IsValid)
                {
                    var guardianType = new GuardianType()
                    {
                        Name = model.Name,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _guardianTypeService.CreateAsync(guardianType);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Guardian Type successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the guardian type.");
                _logger.LogError($@"An error occurred while trying to create the guardian type - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var guardianType = _guardianTypeService.GetById(id);
                if (guardianType == null)
                    return NotFound();

                return View(new GuardianTypeEditViewModel
                {
                    Id = guardianType.Id,
                    Name = guardianType.Name,
                    Inactive = guardianType.IsDeleted,
                    CreatedById = guardianType.CreatedById,
                    CreatedBy = GetUserName(guardianType.CreatedById),
                    CreatedDate = guardianType.CreatedDate,
                    ModifiedById = guardianType.ModifiedById,
                    ModifiedBy = GetUserName(guardianType.ModifiedById ?? Guid.Empty),
                    ModifiedDate = guardianType.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the guardian type.");
                _logger.LogError($@"An error occurred while trying to retrieve the guardian type for id (id) - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GuardianTypeEditViewModel model)
        {
            try
            {
                var existing = _guardianTypeService.GetByName(model.Name);
                if (existing != null && existing.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Name), "Guardian type already exists");

                if (ModelState.IsValid)
                {
                    var guardianType = _guardianTypeService.GetById(model.Id);
                    if (guardianType == null)
                        return NotFound();

                    guardianType.Name = model.Name;
                    guardianType.IsDeleted = model.Inactive;
                    guardianType.ModifiedById = model.ModifiedById;
                    guardianType.ModifiedDate = model.ModifiedDate;

                    await _guardianTypeService.EditAsync(guardianType);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Guardian Type successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the guardian type.");
                _logger.LogError($@"An error occurred while trying to edit the account status for id (id) - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var guardianType = _guardianTypeService.GetById(id);
                if (guardianType == null)
                    return NotFound();

                return PartialView("_Delete", new GuardianTypeDeleteViewModel
                {
                    Id = guardianType.Id,
                    Name = guardianType.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the guardian type.");
                _logger.LogError($@"An error occurred while trying to retrieve the guardian type for id (id) - {ex.Message}");
            }

            return PartialView(new GuardianTypeDeleteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(GuardianTypeDeleteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var guardianType = _guardianTypeService.GetById(model.Id);
                    if (guardianType == null)
                        return NotFound();

                    await _guardianTypeService.DeleteAsync(model.Id);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Guardian Type successfully deleted.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the guardian type.");
                _logger.LogError($@"An error occurred while trying to delete the guardian type for id (id) - {ex.Message}");
            }

            return View(model);
        }
    }
}