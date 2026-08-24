using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museo.Models
{
    public class Likes
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public Users  User { get; set; }

        public Items Item { get; set; }

    }
}