using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace the_Mike_Ro_Blog.Models
{
    public class WhoFollowsWhomVM
    {
        public string FollowerName { get; set; }
        public string FolloweeName { get; set; }
        public int Follow_Id { get; set; }
    }
}