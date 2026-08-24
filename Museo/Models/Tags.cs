using System.ComponentModel.DataAnnotations;

namespace Museo.Models
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
        
        public List<Items> Items { get; set; }
    }
}
