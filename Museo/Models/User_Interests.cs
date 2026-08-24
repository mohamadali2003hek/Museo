using System.ComponentModel.DataAnnotations.Schema;

namespace Museo.Models
{
    public class User_Interests
    {
        public string UserId { get; set; }  

        public int TagId { get; set; }


        [ForeignKey("UserId")]
        public Users User { get; set; }


        [ForeignKey("TagId")]
        public Tags Tag { get; set;  }
    }
}
