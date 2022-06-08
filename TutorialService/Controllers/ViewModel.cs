using TutorialService.Models;

namespace TutorialService.Controllers
{
    public class ViewModel
    {
        private int id;
        public int Id { get { return 1; } set { id = value; } }
        public Example? Example { get; set; }
        public Enrollment? Enrollment { get; set; }
    }
}
