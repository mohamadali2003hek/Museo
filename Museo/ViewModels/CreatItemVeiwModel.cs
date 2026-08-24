using Museo.Models;
using System.ComponentModel.DataAnnotations;

namespace Museo.ViewModels
{
    public class CreatItemVeiwModel
    {
        [Required(ErrorMessage = "URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Url(ErrorMessage = "Please enter a valid preview image URL.")]
        public string? Preview_Image { get; set; }

        [Required(ErrorMessage = "Source is required.")]
        public source_type Source { get; set; }

        [StringLength(1000, ErrorMessage = "Note cannot exceed 1000 characters.")]
        public string? Note { get; set; }

        public float Pos_x { get; set; }

        public float Pos_y { get; set; }
    }
}