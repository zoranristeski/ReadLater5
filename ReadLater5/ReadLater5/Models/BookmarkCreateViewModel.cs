using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ReadLater5.Models
{
    public class BookmarkCreateViewModel
    {
        public Bookmark Bookmark { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public int SelectedCategoryID { get; set; }
        public string NewCategory { get; set; }
    }
}
