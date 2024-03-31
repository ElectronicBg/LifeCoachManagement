using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeCoachManagement.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [ForeignKey("Category")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [EnumDataType(typeof(Status))]
        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [Display(Name = "Статус")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [Display(Name = "Желани резултати")]
        public string DesiredResults { get; set; }

        [Display(Name = "Дата на подаване")]
        public DateTime SubmissionDate { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [FutureDate(ErrorMessage = "Крайната дата трябва да бъде в бъдеще.")]
        [Display(Name = "Крайна дата")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително.")]
        [Range(0, double.MaxValue, ErrorMessage = "Бюджетът не може да бъде отрицателен.")]
        [Display(Name = "Бюджет")]
        public decimal Budget { get; set; }

        public ICollection<Photo> Photos;

        [Display(Name = "Създател")]
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        [ForeignKey("AssignedUser")]
        [Display(Name = "Назначен Треньор")]
        public string AssignedUserId { get; set; }
        public ApplicationUser AssignedUser { get; set; }
    }
}
