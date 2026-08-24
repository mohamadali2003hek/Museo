using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Museo.Models
{
    public class Users: IdentityUser
    {
       
        public string? AvatarUrl { get; set; }

        public string? Bio { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public List<Likes>? Likes { get; set; }
        public List<Saves>? Saves { get; set; }
        public List<Comments>? Comments { get; set; }




    }
}
