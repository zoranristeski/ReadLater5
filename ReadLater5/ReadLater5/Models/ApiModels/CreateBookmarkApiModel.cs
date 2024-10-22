using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ReadLater5.Models
{
    public class CreateBookmarkApiModel
    {
        public string URL { get; set; }

        public string ShortDescription { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }
    }
}
