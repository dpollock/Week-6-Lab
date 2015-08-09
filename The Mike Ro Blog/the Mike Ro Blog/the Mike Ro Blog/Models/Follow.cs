using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace the_Mike_Ro_Blog.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public ApplicationUser Follower { get; set; }

        [NotMapped]
        public ApplicationUser Followee { get; set; }

        public string Follower_Id { get; set; }
       
        public string Followee_Id { get; set; }
       
        

    }
}