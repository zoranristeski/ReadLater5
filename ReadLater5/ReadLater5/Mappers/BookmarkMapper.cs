using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadLater5.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReadLater5.Mappers
{
    public class BookmarkMapper
    {
        public BookmarkViewModel ToBookmarkViewModel(Bookmark bookmark)
        {
            return new BookmarkViewModel
            {
                ID = bookmark.ID,
                URL = bookmark.URL,
                CategoryName = bookmark.Category.Name,
                ShortDescription = bookmark.ShortDescription
            };
        }
        
        public List<BookmarkViewModel> ToBookmarkViewModelList(List<Bookmark> bookmarks)   
        {
            return bookmarks.Select(ToBookmarkViewModel).ToList();
          
        }
       
        public Bookmark ApiBookmarkModelToBookmark(CreateBookmarkApiModel bookmarkApiModel)  
        {
            return new Bookmark
            {
                URL = bookmarkApiModel.URL,
                CategoryId = bookmarkApiModel.CategoryId,
                ShortDescription = bookmarkApiModel.ShortDescription
            };
        }
    }
}
