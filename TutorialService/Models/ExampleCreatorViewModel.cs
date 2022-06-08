using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TutorialService.Models
{
    public class ExampleCreatorViewModel
    {
        public List<Example>? Examples { get; set; }
        public SelectList? Creators { get; set; }
        public string? Creator { get; set; }
        public string? SearchString { get; set; }
    }
}
