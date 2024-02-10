using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LifeCoachManagement.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Category")]
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

        [ForeignKey("AssignedUser")]
        public string AssignedUserId { get; set; }
        public IdentityUser AssignedUser { get; set; }

        //Field to get current user needed
    }
}
