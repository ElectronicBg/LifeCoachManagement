using LifeCoachManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.ViewModels
{
    public class EditAssignmentViewModel
    {
        public Assignment Assignment { get; set; }
        public Photo Photo { get; set; }
    }
}
