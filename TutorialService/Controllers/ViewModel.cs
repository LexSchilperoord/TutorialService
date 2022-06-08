using TutorialService.Models;

namespace TutorialService.Controllers
{
    public class ViewModel
    {
        public int Id { get; set; }
        public Example? Example { get; set; }
        public Enrollment? Enrollment { get; set; }
    }
}
