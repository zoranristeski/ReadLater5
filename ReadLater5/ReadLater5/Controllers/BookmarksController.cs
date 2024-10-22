using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class BookmarksController : Controller
    {
        IBookmarkService _bookmarkService;
        ICategoryService _categoryService;
        UserManager<ApplicationUser> _userManager;
       
        public BookmarksController(IBookmarkService bookmarkService, ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
            _userManager = userManager;
        }
        // GET: Bookmarks
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            List<Bookmark> bookmarks = await _bookmarkService.GetBookmarks(userId);
            BookmarkMapper mapper = new BookmarkMapper();       
            return View(mapper.ToBookmarkViewModelList(bookmarks));
        }

        // GET: Bookmarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            var bookmark = await _bookmarkService.GetBookmark(id.Value, userId);
            var categories = await _categoryService.GetCategories(userId);
            var viewModel = new BookmarkCreateViewModel
            {
                Bookmark = bookmark,
                SelectedCategoryID = bookmark.CategoryId.Value,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToList()
            };

            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }

            return View(viewModel);

        }

        // GET: Bookmarks/Create
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            var categories = await _categoryService.GetCategories(userId);

            // Prepare ViewModel
            var viewModel = new BookmarkCreateViewModel
            {
                Bookmark = new Bookmark(), 
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToList() 
            };

            return View(viewModel);       
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]  BookmarkCreateViewModel bookmarkViewModel)
        {
            var userId = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(bookmarkViewModel.NewCategory))
            {              
                var category = await _categoryService.CreateCategory(bookmarkViewModel.NewCategory, userId);
                bookmarkViewModel.Bookmark.CategoryId = category.ID;   
                bookmarkViewModel.SelectedCategoryID = category.ID;
            }

            if (ModelState.IsValid)
            {
                bookmarkViewModel.Bookmark.UserId = userId;
                await _bookmarkService.CreateBookmark(bookmarkViewModel.Bookmark);
                return RedirectToAction("Index"); 
            }

            var categories = await _categoryService.GetCategories(userId);
            bookmarkViewModel.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            return View(bookmarkViewModel);          
        }

        // GET: Bookmarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            var bookmark = await _bookmarkService.GetBookmark(id.Value, userId);
            var categories = await _categoryService.GetCategories(userId);
            var viewModel = new BookmarkCreateViewModel
            {
                Bookmark = bookmark,
                SelectedCategoryID = bookmark.CategoryId.Value, 
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToList()
            };

            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            
            return View(viewModel);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookmarkCreateViewModel bookmarkViewModel)
        {
            var userId = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(bookmarkViewModel.NewCategory))
            {
                var category = await _categoryService.CreateCategory(bookmarkViewModel.NewCategory, userId);
                bookmarkViewModel.Bookmark.CategoryId = category.ID;
                bookmarkViewModel.SelectedCategoryID = category.ID;
            }

            if (ModelState.IsValid)
            {
                bookmarkViewModel.Bookmark.CategoryId = bookmarkViewModel.SelectedCategoryID;
                bookmarkViewModel.Bookmark.UserId = userId;
                await _bookmarkService.UpdateBookmark(bookmarkViewModel.Bookmark,userId);
                return RedirectToAction("Index");
            }

            var categories = await _categoryService.GetCategories(userId);
            bookmarkViewModel.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();

            return View(bookmarkViewModel);
        }

        // GET: Bookmarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            var bookmark = await _bookmarkService.GetBookmark(id.Value, userId);
            var categories = await _categoryService.GetCategories(userId);
            var viewModel = new BookmarkCreateViewModel
            {
                Bookmark = bookmark,
                SelectedCategoryID = bookmark.CategoryId.Value,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToList()
            };

            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }

            return View(viewModel);           
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            Bookmark bookmark = await _bookmarkService.GetBookmark(id, userId);
            await _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
    }
}
