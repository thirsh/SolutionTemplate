using System;

namespace SolutionTemplate.BusinessModel
{
    public class DoodadGet
    {
        public int Id { get; set; }
        public int WidgetId { get; set; }
        public WidgetGet Widget { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}