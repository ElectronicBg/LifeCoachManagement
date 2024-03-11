using LifeCoachManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.ViewModels
{
    public class CoachEditAssignmentViewModel
    {
        public int Id { get; set; }

        //[Display(Name = "Photo")]
        public IFormFile FileUpload { get; set; }

        //[Display(Name = "Status")]
        public Status Status { get; set; }
    }
}
