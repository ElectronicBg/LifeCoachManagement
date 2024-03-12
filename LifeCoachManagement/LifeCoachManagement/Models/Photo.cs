using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public IFormFile FileUpload { get; set; }
        public string FileName { get; set; }
        public DateTime CreationDate { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
