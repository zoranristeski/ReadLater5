using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ReadLater5.Models
{
    public class BookmarkViewModel
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public string ShortDescription { get; set; }
        public string CategoryName { get; set; }
        public int SelectedCategoryID { get; set; }
        public SelectList Categories { get; set; }
    }
}
