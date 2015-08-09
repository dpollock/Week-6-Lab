using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace the_Mike_Ro_Blog.Models
{
    public class PostVM
    {
        public string Poster { get; set; }
        public DateTime PostedOn { get; set; }
        public string Text { get; set; }
        public int Post_Id { get; set; }
        public bool IsMine { get; set; }
    }
}