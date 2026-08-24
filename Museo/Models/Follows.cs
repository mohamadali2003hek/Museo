using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museo.Models
{
    public class Follows
    {
        [ForeignKey("User")]
        public string follower_id { get; set; }

        [ForeignKey("User")]
        public string following_id { get; set; }

        public Users follower { get; set; }
        public Users following { get; set; }


    }
}
