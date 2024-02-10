using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
