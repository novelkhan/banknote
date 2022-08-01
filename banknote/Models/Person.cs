namespace banknote.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Note>? ResearvedNotes { get; set; }
    }
}