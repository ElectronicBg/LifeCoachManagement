using LifeCoachManagement.Data;
using LifeCoachManagement.Models;
using LifeCoachManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeCoachManagement.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AssignmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Assignment
        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignments
                .Include(a => a.Category)
                .Include(a => a.AssignedUser)
            .ToListAsync();

            return View(assignments);
        }

        // GET: Assignment/Create
        public IActionResult Create()
        {
            ViewBag.Categories= _context.Categories.ToList();
            return View();
        }

        // POST: Assignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                // Set status to pending
                assignment.Status = Status.Pending;

                // Assign current user to the assignment if applicable
                if (User.Identity.IsAuthenticated)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    assignment.AssignedUserId = currentUser.Id;
                }

                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(assignment);
        }

        // Update Assignment
        public IActionResult Edit(int id)
        {
            var assignment = _context.Assignments
              .Include(a => a.Category)
              .FirstOrDefault(a => a.Id == id);

            var photo = new Photo();

            var model = new EditAssignmentViewModel { Photo = photo, Assignment = assignment };
                

            if (assignment == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAssignmentViewModel editedAssignment)
        {
            if (id != editedAssignment.Assignment.Id)
            {
                return NotFound();
            }

            // Assign current user to the assignment if applicable
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                editedAssignment.Assignment.AssignedUserId = currentUser.Id;
            }

            if (ModelState.IsValid)
            {
                var existingAssignment = _context.Assignments
                    .Include(a => a.Category)
                    .FirstOrDefault(a => a.Id == id);

                if (existingAssignment == null)
                {
                    return NotFound();
                }


                _context.Entry(existingAssignment).CurrentValues.SetValues(editedAssignment.Assignment);


                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var assignment = _context.Assignments.Find(id);

            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }
    }
}
