namespace SolutionTemplate.BusinessModel
{
    public class DoodadPost
    {
        public int WidgetId { get; set; }
        public WidgetPost Widget { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}