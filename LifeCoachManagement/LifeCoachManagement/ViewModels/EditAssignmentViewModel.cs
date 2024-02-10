using LifeCoachManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.ViewModels
{
    public class EditAssignmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
        public string Description { get; set; }
        public string DesiredResults { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Budget { get; set; }

        public ICollection<Photo> Photos;
        public IFormFile FileUpload { get; set; }
        public string AssignedUserId { get; set; }
        public IdentityUser AssignedUser { get; set; }
    }
}
