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
        public IActionResult Index(int assignmentId)
        {
            var photos = _context.Photos
                .Where(p => p.AssignmentId == assignmentId)
                .ToList();
            return View(photos);
        }
        [HttpPost]
        public IActionResult Create(CoachEditAssignmentViewModel viewModel)
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
                    FileName = fileName,
                    FileUpload = viewModel.FileUpload,
                    CreationDate=viewModel.SubmissionDate,
                    Status = viewModel.Status,
                    AssignmentId = viewModel.Id                 
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

