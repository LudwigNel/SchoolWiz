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
using SchoolWiz.WebApp.Models.Grade;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GradeController : ApplicationBaseController
    {
        private readonly IGradeService _gradeService;
        private readonly ILogger _logger;

        public GradeController(IGradeService gradeService, UserManager<ApplicationUser> userManager, ILoggerFactory factory)
        : base(userManager)
        {
            _gradeService = gradeService;
            _logger = factory.CreateLogger("Grade");
        }

        public IActionResult Index()
        {
            try
            {
                var grades = _gradeService.GetAll(true).Select(g => new GradeIndexViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    InActive = g.IsDeleted
                });
                return View(grades);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the grade list.");
                _logger.LogError($@"An error occurred while trying to retrieve the grade list - {ex.Message}");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new GradeCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeCreateViewModel model)
        {
            try
            {
                var existing = _gradeService.GetByName(model.Name);
                if(existing != null)
                    ModelState.AddModelError(nameof(model.Name), "Grade already exists.");

                if (ModelState.IsValid)
                {
                    var grade = new Grade
                    {
                        Name = model.Name,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _gradeService.CreateAsync(grade).ConfigureAwait(false);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Grade succssfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to create a new grade - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var grade = _gradeService.GetById(id);
                if (grade == null)
                    return NotFound();

                return View(new GradeEditViewModel
                {
                    Id = grade.Id,
                    Name = grade.Name,
                    Inactive = grade.IsDeleted,
                    CreatedById = grade.CreatedById,
                    CreatedBy = GetUserName(grade.CreatedById),
                    CreatedDate = grade.CreatedDate,
                    ModifiedById = grade.ModifiedById,
                    ModifiedBy = GetUserName(grade.ModifiedById ?? Guid.Empty),
                    ModifiedDate = grade.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the grade.");
                _logger.LogError($@"An error occurred while trying to retrieve the grade for id (id) - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GradeEditViewModel model)
        {
            try
            {
                var existing = _gradeService.GetByName(model.Name);
                if (existing != null && existing.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Name), "Grade already exists.");

                if (ModelState.IsValid)
                {
                    var grade = _gradeService.GetById(model.Id);
                    if (grade == null)
                        return NotFound();

                    grade.Name = model.Name;
                    grade.IsDeleted = model.Inactive;
                    grade.ModifiedById = model.ModifiedById;
                    grade.ModifiedDate = model.ModifiedDate;

                    await _gradeService.EditAsync(grade);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Grade successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the grade.");
                _logger.LogError($@"An error occurred while trying to edit the grade for id (id) - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var grade = _gradeService.GetById(id);
                if (grade == null)
                    return NotFound();

                return PartialView("_Delete", new GradeDeleteViewModel
                {
                    Id = grade.Id,
                    Name = grade.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the grade.");
                _logger.LogError($@"An error occurred while trying to retrieve the grade for id (id) - {ex.Message}");
            }

            return PartialView("_Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(GradeDeleteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var grade = _gradeService.GetById(model.Id);
                    if (grade == null)
                        return NotFound();

                    await _gradeService.DeleteAsync(model.Id);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Grade successfully deleted.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the grade.");
                _logger.LogError($@"An error occurred while trying to delete the grade for id (id) - {ex.Message}");
            }

            return View();
        }
    }
}