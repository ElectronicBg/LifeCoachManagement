using LifeCoachManagement.Data;
using LifeCoachManagement.Models;
using LifeCoachManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LifeCoachManagement.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AssignmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Assignment
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            List<Assignment> assignments;

            // Check the user's role and fetch assignments accordingly
            if (User.IsInRole(Roles.Admin.ToString()))
            {
                assignments = await _context.Assignments
                    .Include(a => a.Category)
                    .Include(a => a.Creator)
                    .Include(a => a.AssignedUser)
                    .ToListAsync();
            }
            else if (User.IsInRole(Roles.Coach.ToString()))
            {
                assignments = await _context.Assignments
                    .Include(a => a.Category)
                    .Include(a => a.Creator)
                    .Where(a => a.AssignedUserId == currentUser.Id)
                    .ToListAsync();
            }
            else if (User.IsInRole(Roles.Client.ToString()))
            {
                assignments = await _context.Assignments
                    .Include(a => a.Category)
                    .Include(a => a.Creator)
                    .Include(a => a.AssignedUser)
                    .Where(a => a.CreatorId == currentUser.Id)
                    .ToListAsync();
            }
            else
            {
                // Handle unauthenticated or unauthorized users
                return RedirectToAction("AccessDenied", "Account");
            }

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();

            return View(assignments);
        }
        [HttpGet]
        public async Task<IActionResult> AssignmentPhotos(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            var photos = await _context.Photos
                .Where(p => p.AssignmentId == id)
                .ToListAsync();

            return View(photos);
        }

        // GET: Assignment/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
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
                    assignment.CreatorId = currentUser.Id;
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

            if (assignment == null)
            {
                return NotFound();
            }

            var usersWithCoachRole = _userManager.GetUsersInRoleAsync(Roles.Coach.ToString()).Result;


            var model = new EditAssignmentViewModel
            {
                Assignment = assignment,
                AssignedUsers = new SelectList(usersWithCoachRole, "Id", "UserName")
            };

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
                editedAssignment.Assignment.CreatorId = currentUser.Id;

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

                var assignedUser = await _userManager.FindByIdAsync(editedAssignment.Assignment.AssignedUserId);

                // Set the AssignedUser property of the assignment
                editedAssignment.Assignment.AssignedUser = assignedUser;

                _context.Entry(existingAssignment).CurrentValues.SetValues(editedAssignment.Assignment);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();

            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CoachEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            var viewModel = new CoachEditAssignmentViewModel
            {
                Id = assignment.Id,
                Status = assignment.Status,
            };

            return View("CoachEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CoachEdit(CoachEditAssignmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assignment = await _context.Assignments.FindAsync(viewModel.Id);

            if (assignment == null)
            {
                return NotFound();
            }

            assignment.Status = viewModel.Status;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string newStatus)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            if (Enum.TryParse<Status>(newStatus, out var status))
            {
                assignment.Status = status;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("Invalid status");
            }
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
