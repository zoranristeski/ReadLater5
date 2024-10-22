using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        public BookmarkService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }
        public async Task<Bookmark> CreateBookmark(Bookmark bookmark)
        {
            await _ReadLaterDataContext.AddAsync(bookmark);
            await _ReadLaterDataContext.SaveChangesAsync();
            return bookmark;
        }

        public async Task<bool> DeleteBookmark(Bookmark bookmark)
        {
            try
            {
                _ReadLaterDataContext.Bookmark.Remove(bookmark);
                await _ReadLaterDataContext.SaveChangesAsync();
                return true;
            }catch (Exception ex) { return false; }
        }

        public async Task<Bookmark> GetBookmark(int Id, string userID)
        {
            return await _ReadLaterDataContext.Bookmark.Where(b => b.ID == Id && b.UserId == userID).Include(x => x.Category).FirstOrDefaultAsync();
        }

        public async Task<Bookmark> GetBookmark(string url, string userID)
        {
            return await _ReadLaterDataContext.Bookmark.Where(b => b.URL == url && b.UserId == userID).FirstOrDefaultAsync();
        }

        public async Task<List<Bookmark>> GetBookmarks(string userID)
        {
            return await _ReadLaterDataContext.Bookmark.Where(b => b.UserId == userID).Include(x => x.Category).ToListAsync();
        }
        public async Task<List<BookmarkDTO>> GetApiBookmarks()
        {
            var bookmarks = await _ReadLaterDataContext.Bookmark.Include(x => x.Category).ToListAsync();

            return bookmarks.Select(b => new BookmarkDTO
            {
                ID = b.ID,
                URL = b.URL,
                ShortDescription = b.ShortDescription,
                CategoryName = b.Category.Name
            }).ToList();          
        }
        public async Task<Bookmark> GetApiBookmark(int bookmarkID)
        {
            return await _ReadLaterDataContext.Bookmark.Where(b => b.ID == bookmarkID).Include(x => x.Category).FirstOrDefaultAsync();
        }
        public async Task<bool> PutApiBookmark(Bookmark bookmark)
        {
            try
            {
                var existingBookmark = await _ReadLaterDataContext.Bookmark.FirstOrDefaultAsync(b => b.ID == bookmark.ID);

                if (existingBookmark == null)
                {
                    return false; 
                }
         
                existingBookmark.URL = bookmark.URL;
                existingBookmark.ShortDescription = bookmark.ShortDescription;
                existingBookmark.CategoryId = bookmark.CategoryId;

                await _ReadLaterDataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
}
        public async Task<bool> UpdateBookmark(Bookmark bookmark, string userId)
        {
            try
            {
                bookmark.UserId = userId;
                
                _ReadLaterDataContext.Bookmark.Attach(bookmark);

                _ReadLaterDataContext.Entry(bookmark).State = EntityState.Modified;
                _ReadLaterDataContext.Entry(bookmark).Property(b => b.CreateDate).IsModified = false;
                
                //_ReadLaterDataContext.Update(bookmark);
                await _ReadLaterDataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
