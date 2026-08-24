using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museo.Models
{
    public class Saves
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("ItemId")]
        public Items Item { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }



        public DateTime Saved_at { get; set; } = DateTime.Now;
    }
}
