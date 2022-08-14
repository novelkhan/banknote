using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace banknote.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public int NoteValue { get; set; }



        public int Id { get; set; }
    }
}