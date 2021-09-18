using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;
using SchoolWiz.Services;
using SchoolWiz.Web.Filters;
using SchoolWiz.Web.Models.AddressType;

namespace SchoolWiz.Web.Controllers
{
    public class AddressTypeController : ApplicationBaseController
    {
        private readonly IAddressTypeService _addressTypeService;
        private readonly UserManager<ApplicationUser> _usrUserManager;
        private readonly ILogger<AddressTypeController> _logger;

        public AddressTypeController(ApplicationDbContext context, IAddressTypeService addressTypeService, UserManager<ApplicationUser> usrUserManager, ILogger<AddressTypeController> logger) 
            : base(usrUserManager)
        {
            _addressTypeService = addressTypeService;
            _usrUserManager = usrUserManager;
            _logger = logger;
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
                if (ModelState.IsValid)
                {
                    var addressType = new AddressType
                    {
                        Name = model.Name, 
                        IsDeleted = model.Inactive, 
                        CreatedById = model.CreatedById, 
                        CreatedDate =  model.CreatedDate
                    };

                    await _addressTypeService.CreateAsync(addressType);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to create the address type - {ex.Message}");
            }

            return View();
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to edit the address type with id {model.Id} - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var addressType = _addressTypeService.GetById(id);
                if (addressType == null)
                    return NotFound();

                return View(new AddressTypeDeleteViewModel
                {
                    Id = addressType.Id,
                    Name = addressType.Name
                });
            }
            catch (Exception ex)
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to delete the address type with id {model.Id} - {ex.Message}");
            }

            return View();
        }
    }

}