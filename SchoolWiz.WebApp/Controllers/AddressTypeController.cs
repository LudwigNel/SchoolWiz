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
using SchoolWiz.Persistence;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Models.AddressType;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AddressTypeController : ApplicationBaseController
    {
        private readonly IAddressTypeService _addressTypeService;
        private readonly UserManager<ApplicationUser> _usrUserManager;
        private readonly ILogger _logger;

        public AddressTypeController(ApplicationDbContext context, IAddressTypeService addressTypeService, UserManager<ApplicationUser> usrUserManager, ILoggerFactory factory)
            : base(usrUserManager)
        {
            _addressTypeService = addressTypeService;
            _usrUserManager = usrUserManager;
            _logger = factory.CreateLogger("AddressType");
        }

        public IActionResult Index()
        {
            try
            {
                var addressTypes = _addressTypeService.GetAll(true).Select(addressType => new AddressTypeIndexViewModel
                {
                    Id = addressType.Id,
                    Name = addressType.Name,
                    InActive = addressType.IsDeleted
                });

                return View(addressTypes);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(), "Error",
                    "Something went wrong while trying to retrieve the address type list.");
                _logger.LogError($@"An error occurred while trying to retrieve the address type list - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AddressTypeCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressTypeCreateViewModel model)
        {
            try
            {
                var existing = _addressTypeService.GetByName(model.Name);
                if (existing != null)
                    ModelState.AddModelError(nameof(model.Name), "Address type already exists.");

                if (ModelState.IsValid)
                {
                    var addressType = new AddressType
                    {
                        Name = model.Name,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };

                    await _addressTypeService.CreateAsync(addressType);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "Address Type successfully created");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(), "Error",
                    "Something went wrong while trying to create the address type.");
                _logger.LogError($@"An error occurred while trying to create the address type - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var addressType = _addressTypeService.GetById(id);
                if (addressType == null)
                    return NotFound();

                return View(new AddressTypeEditViewModel
                {
                    Id = addressType.Id,
                    Name = addressType.Name,
                    Inactive = addressType.IsDeleted,
                    CreatedById = addressType.CreatedById,
                    CreatedBy = GetUserName(addressType.CreatedById),
                    CreatedDate = addressType.CreatedDate,
                    ModifiedById = addressType.ModifiedById,
                    ModifiedBy = GetUserName(addressType.ModifiedById ?? Guid.Empty),
                    ModifiedDate = addressType.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(), "Error",
                    "Something went wrong while trying to retrieve the address type.");
                _logger.LogError($@"An error occurred while trying to retrieve the address type with id {id} - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddressTypeEditViewModel model)
        {
            try
            {
                var existing = _addressTypeService.GetByName(model.Name);
                if (existing != null && existing.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Name), "Address type already exists.");

                if (ModelState.IsValid)
                {
                    var addressType = _addressTypeService.GetById(model.Id);
                    if (addressType == null)
                        return NotFound();

                    addressType.Name = model.Name;
                    addressType.IsDeleted = model.Inactive;
                    addressType.ModifiedById = model.ModifiedById;
                    addressType.ModifiedDate = model.ModifiedDate;

                    await _addressTypeService.EditAsync(addressType);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "Address Type successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(), "Error",
                    "Something went wrong while trying to edit the address type.");
                _logger.LogError($@"An error occurred while trying to edit the address type with id {model.Id} - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var addressType = _addressTypeService.GetById(id);
                if (addressType == null)
                    return NotFound();

                return PartialView("_Delete", new AddressTypeDeleteViewModel
                {
                    Id = addressType.Id,
                    Name = addressType.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(), "Error",
                    "Something went wrong while trying to retrieve the address type.");
                _logger.LogError($@"An error occurred while trying to retrieve the address type with id {id} - {ex.Message}");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AddressTypeDeleteViewModel model)
        {
            try
            {
                await _addressTypeService.DeleteAsync(model.Id);
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                    "Address Type successfully deleted.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(), "Error",
                    "Something went wrong while trying to delete the address type.");
                _logger.LogError($@"An error occurred while trying to delete the address type with id {model.Id} - {ex.Message}");
            }

            return View();
        }
    }

}