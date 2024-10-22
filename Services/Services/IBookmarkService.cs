using Entity;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookmarkService
    {
        Task<Bookmark> CreateBookmark(Bookmark bookmark);
        Task<List<Bookmark>> GetBookmarks(string userId);
        Task<Bookmark> GetBookmark(int Id, string userID);
        Task<Bookmark> GetBookmark(string Name, string userID);
        Task<bool> UpdateBookmark(Bookmark bookmark, string userId);
        Task<bool> DeleteBookmark(Bookmark bookmark);
        Task<List<BookmarkDTO>> GetApiBookmarks();
        Task<Bookmark> GetApiBookmark(int bookmarkID);
        Task<bool> PutApiBookmark(Bookmark bookmark);
    }
}
