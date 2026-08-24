using System.ComponentModel.DataAnnotations;

namespace Museo.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        public string Bio { get; set; }
    }
}
