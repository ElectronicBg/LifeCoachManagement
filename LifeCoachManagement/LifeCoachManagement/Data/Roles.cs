using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.Data
{
    public enum Roles
    {
        [Display(Name = "Администратор")]
        Admin,
        [Display(Name = "Треньор")]
        Coach,
        [Display(Name = "Клиент")]
        Client,
    }
}
