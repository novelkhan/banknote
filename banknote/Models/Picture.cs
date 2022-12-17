namespace banknote.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string PictureName { get; set; }

        public byte[] Image { get; set; }

        public string UserId { get; set; }
    }
}
