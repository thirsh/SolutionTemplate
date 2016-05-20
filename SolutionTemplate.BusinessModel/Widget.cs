using System;
using System.Collections.Generic;

namespace SolutionTemplate.BusinessModel
{
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ICollection<Doodad> Doodads { get; set; }
    }
}