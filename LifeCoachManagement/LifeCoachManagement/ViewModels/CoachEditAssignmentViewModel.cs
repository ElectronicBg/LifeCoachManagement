using LifeCoachManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.ViewModels
{
    public class CoachEditAssignmentViewModel
    {
        public int Id { get; set; }
        public IFormFile FileUpload { get; set; }
        public Status Status { get; set; }
    }
}
