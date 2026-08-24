using Museo.Models;
using System.ComponentModel.DataAnnotations;

namespace Museo.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Preview_Image { get; set; }
        public source_type Source { get; set; }
        public string  Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public float Pos_x { get; set; }
        public float Pos_y { get; set; }
    }
}
