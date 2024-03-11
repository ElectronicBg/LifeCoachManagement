using LifeCoachManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LifeCoachManagement.ViewModels
{
    public class EditAssignmentViewModel
    {
        public Assignment Assignment { get; set; }
        public SelectList AssignedUsers { get; set; }
    }
}
