using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace banknote.ViewModels
{
    public class BookModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of your book")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the author name")]
        public string Author { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public string Category { get; set; }

        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string CoverImageUrl { get; set; }



        [Display(Name = "Choose the gallery images of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }


        public List<GalleryModel> Gallery { get; set; }
    }
}