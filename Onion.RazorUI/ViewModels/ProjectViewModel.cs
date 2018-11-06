namespace Onion.RazorUI.ViewModels
{
    public class ProjectViewModel : IViewModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
