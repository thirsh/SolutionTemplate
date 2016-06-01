using System;
using System.Collections.Generic;

namespace SolutionTemplate.BusinessModel
{
    public class WidgetGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ICollection<DoodadGet> Doodads { get; set; }
    }
}