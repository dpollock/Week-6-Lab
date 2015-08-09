using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace the_Mike_Ro_Blog.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }

        
        public ApplicationUser Follower { get; set; }

      
        public ApplicationUser Followee { get; set; }

      
        public DateTime Since { get; set; }

        public string Follower_Id { get; set; }

        public string Followee_Id { get; set; }
    }
}