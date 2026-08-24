using Museo.Models;

namespace Museo.ViewModels
{
    public class ProfilesViewModel
    {
        public string Title { get; set; }

        public bool IsPrivate { get; set; }

        public string Theme { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}