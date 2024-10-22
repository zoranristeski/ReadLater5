using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity
{
    public class Bookmark
    {
        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreateDate { get; set; }

        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }

        public string UserId { get; set; }  
        public ApplicationUser User { get; set; }
    }
}
