﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace the_Mike_Ro_Blog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(151)]
        public string Text { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        [Required]
        public ApplicationUser Poster { get; set; }

    }
}