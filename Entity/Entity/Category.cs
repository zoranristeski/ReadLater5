﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User  { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }  
    }
}
