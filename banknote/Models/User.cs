using System.ComponentModel.DataAnnotations;

namespace banknote.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public List<Picture>? Pictures { get; set; }
    }
}
