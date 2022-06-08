using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorialService.Models
{
    public class Example
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime Creation { get; set; }
        public string? Creator { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(10)]
        public string? Rating { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
