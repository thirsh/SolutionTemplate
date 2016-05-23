using System;
using System.Collections.Generic;

namespace SolutionTemplate.DataModel
{
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }

        public virtual ICollection<Doodad> Doodads { get; set; }
    }
}