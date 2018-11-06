namespace Onion.RazorUI.ViewModels
{
    public class EntryViewModel : IViewModel
    {
        public int id { get; set; }
        public int personid { get; set; }
        public int projectid { get; set; }
        public int hours { get; set; }

        public virtual PersonViewModel person { get; set; }
        public virtual ProjectViewModel project { get; set; }
    }
}
