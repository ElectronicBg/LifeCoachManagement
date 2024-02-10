using LifeCoachManagement.Data;
using LifeCoachManagement.Models;
using LifeCoachManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LifeCoachManagement.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditAssignmentViewModel viewModel)
        {
            if (viewModel.FileUpload != null && viewModel.FileUpload.Length > 0)
            {
                // Process and save the file
                var fileName = Path.GetFileName(viewModel.FileUpload.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    viewModel.FileUpload.CopyTo(stream);
                }

                var photo = new Photo
                {
                    Status = viewModel.Status,
                    AssignmentId = viewModel.Id,
                    FileUpload = viewModel.FileUpload
                };

                // Add the photo to the database
                _context.Photos.Add(photo);
                _context.SaveChanges();

                return RedirectToAction("Assignment", "Index");
            }
            else
            {
                // Handle invalid file upload
                ModelState.AddModelError("FileUpload", "Please choose a valid file.");
                return View();
            }
        }
    }
}

