using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museo.Models
{
    public class Profiles
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Is_private { get; set; } = false;

        public  string Theme { get; set; }

        public DateTime Created_at { get; set; }= DateTime.Now;

        
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public Users  User { get; set; }
    }
}
