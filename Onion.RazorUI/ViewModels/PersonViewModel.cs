namespace Onion.RazorUI.ViewModels
{
    public class PersonViewModel : IViewModel
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fathername { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
