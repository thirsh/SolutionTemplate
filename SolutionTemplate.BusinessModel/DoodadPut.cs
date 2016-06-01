namespace SolutionTemplate.BusinessModel
{
    public class DoodadPut
    {
        public int Id { get; set; }
        public int WidgetId { get; set; }
        public WidgetPut Widget { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}