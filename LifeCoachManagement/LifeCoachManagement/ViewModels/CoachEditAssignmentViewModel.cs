using LifeCoachManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.ViewModels
{
    public class CoachEditAssignmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Прикачете Снимка!")]
        public IFormFile FileUpload { get; set; }
        public DateTime SubmissionDate { get; set; }
        public Status Status { get; set; }
    }
}
