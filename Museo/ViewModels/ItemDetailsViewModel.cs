using Museo.Models;

namespace Museo.ViewModels
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }

        public string Note { get; set; }

        public source_type Source { get; set; }

        public DateTime Created_at { get; set; }

        public List<Comments> Comments { get; set; }

        public List<Likes> Likes { get; set; }



    }
}
