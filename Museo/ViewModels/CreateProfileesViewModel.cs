using System.ComponentModel.DataAnnotations;

namespace Museo.ViewModels
{
    public class CreateProfilesViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public bool IsPrivate { get; set; }

        public string Theme { get; set; }
    }

}

