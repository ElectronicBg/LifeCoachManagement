using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [Display(Name = "Име")]
        public string Name { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
