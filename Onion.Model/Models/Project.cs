using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Model
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
