using LifeCoachManagement.Data;
using LifeCoachManagement.Models;
using LifeCoachManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LifeCoachManagement.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(EditAssignmentViewModel viewModel)
        {        
            if (viewModel.Photo.FileUpload != null && viewModel.Photo.FileUpload.Length > 0)
            {
                // Process and save the file
                var fileName = Path.GetFileName(viewModel.Photo.FileUpload.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    viewModel.Photo.FileUpload.CopyTo(stream);
                }

                var photo = new Photo
                {
                    Status = viewModel.Assignment.Status,
                    AssignmentId = viewModel.Assignment.Id,
                    FileUpload = viewModel.Photo.FileUpload
                };

                // Add the photo to the database
                _context.Photos.Add(photo);
                _context.SaveChangesAsync();

                return Ok();
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

