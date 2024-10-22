using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace ReadLater5.Controllers.Api
{
    [Route("api/bookmarks")]
    [ApiController]
    public class BookmarksApiController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly IConfiguration _configuration;
        
        public BookmarksApiController(IBookmarkService bookmarkService, IConfiguration configuration)
        {
            _bookmarkService = bookmarkService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ValidateToken())
            {
                return Unauthorized();
            }

            var bookmarks = await _bookmarkService.GetApiBookmarks();         
            return Ok(bookmarks);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateBookmarkApiModel createBookmarkApiModel)
        {
            if (!ValidateToken())
            {
                return Unauthorized();
            }

            BookmarkMapper bookmarkMapper = new BookmarkMapper();
            var model = bookmarkMapper.ApiBookmarkModelToBookmark(createBookmarkApiModel);

            await _bookmarkService.CreateBookmark(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateBookmarkApiModel updateBookmarkApiModel)
        {
            if (!ValidateToken())
            {
                return Unauthorized();
            }

            var existingBookmark = await _bookmarkService.GetApiBookmark(id);

            if (existingBookmark == null)
            {
                return NotFound(); 
            }
         
            BookmarkMapper bookmarkMapper = new BookmarkMapper();
            var updatedBookmark = bookmarkMapper.ApiBookmarkModelToBookmark(updateBookmarkApiModel);

            updatedBookmark.ID = existingBookmark.ID;
            updatedBookmark.CreateDate = existingBookmark.CreateDate; 

            var result = await _bookmarkService.PutApiBookmark(updatedBookmark); 

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok(updatedBookmark); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {        
            if (!ValidateToken())
            {
                return Unauthorized();
            }

            var bookmark = await _bookmarkService.GetApiBookmark(id);

            if (bookmark == null)
            {
                return NotFound(); 
            }
           
            var result = await _bookmarkService.DeleteBookmark(bookmark);

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok(); 
        }

        private bool ValidateToken()
        {
            Request.Headers.TryGetValue("Authorization", out var authHeader);

            var token = authHeader.ToString();
            if (string.IsNullOrEmpty(token)) 
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var apiKey = _configuration.GetValue<string>("ApiKey");
            var key = Encoding.ASCII.GetBytes(apiKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
