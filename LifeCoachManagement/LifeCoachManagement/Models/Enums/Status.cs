using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.Models
{
    public enum Status
    {
        [Display(Name = "Чакаща")]
        Pending,
        [Display(Name = "Назначена на треньор")]
        AssignedToCoach,
        [Display(Name = "За преглед")]
        ForReview,
        [Display(Name = "Изпълнена")]
        Completed,
        [Display(Name = "Отказана")]
        Cancelled
    }
}
