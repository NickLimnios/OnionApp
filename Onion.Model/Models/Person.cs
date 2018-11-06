using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Model
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fathername { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
