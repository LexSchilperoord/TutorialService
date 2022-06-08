using System.ComponentModel.DataAnnotations.Schema;

namespace TutorialService.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
