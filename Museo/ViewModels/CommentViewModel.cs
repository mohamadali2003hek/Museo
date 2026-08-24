using System.ComponentModel.DataAnnotations;

namespace Museo.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public string Content { get; set; }
    }
}