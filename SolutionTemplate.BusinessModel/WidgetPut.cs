using System.Collections.Generic;

namespace SolutionTemplate.BusinessModel
{
    public class WidgetPut
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public ICollection<DoodadPut> Doodads { get; set; }
    }
}