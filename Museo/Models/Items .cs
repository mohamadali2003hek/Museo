using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museo.Models
{
    public class Items
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public string Title { get; set; }

        public string Preview_Image { get; set; }

        public source_type Source { get; set; }

        public string Note { get; set; }

        public float Pos_x { get; set; }

        public float Pos_y { get; set; }
        
        public DateTime Created_at { get; set; }= DateTime.Now;


        public List<Tags> Tags { get; set; }
        public List<Likes> Likes { get; set; }
        public List<Saves> Saves { get; set; }

        public List<Comments> Comments { get; set; }


        public int ProfileId { get; set; }


        [ForeignKey("ProfileId")]
        public Profiles Profile { get; set; }


    }

    public enum source_type
    {
        youtube,
        instagram,
        twitter,
        pinterest,

    }
}
