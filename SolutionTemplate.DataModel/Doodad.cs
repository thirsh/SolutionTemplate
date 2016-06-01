using System;

namespace SolutionTemplate.DataModel
{
    public class Doodad
    {
        public int Id { get; set; }
        public int WidgetId { get; set; }
        public virtual Widget Widget { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
    }
}