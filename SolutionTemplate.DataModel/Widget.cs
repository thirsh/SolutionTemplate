using System;
using System.Collections.Generic;

namespace SolutionTemplate.DataModel
{
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Doodad> Doodads { get; set; }
    }
}