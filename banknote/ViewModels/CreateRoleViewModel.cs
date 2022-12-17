using System.ComponentModel.DataAnnotations;

namespace banknote.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
