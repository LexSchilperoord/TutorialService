using System.ComponentModel.DataAnnotations;

namespace TutorialService.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        [Display(Name = "Enrollment ID")]
        public int EnrollmentID { get; set; }
        [Display(Name = "Group ID")]
        public int GroupID { get; set; }
        [Display(Name = "Example ID")]
        public int ExampleID { get; set; }
        public Grade? Grade { get; set; }
        public Group? Group { get; set; }
        public Example? Example { get; set; }
    }
}
