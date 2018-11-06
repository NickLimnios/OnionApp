using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Model
{
    public class Entry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int personid { get; set; }
        public int projectid { get; set; }
        public int hours { get; set; }

        public virtual Person person { get; set; }
        public virtual Project project { get; set; }
    }
}
