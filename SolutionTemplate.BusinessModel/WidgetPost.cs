using System.Collections.Generic;

namespace SolutionTemplate.BusinessModel
{
    public class WidgetPost
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public ICollection<DoodadPost> Doodads { get; set; }
    }
}