using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.Pages.Albums
{
    public class DetailsModel : PageModel
    {
        private readonly SE1611_Group3_A3.Models.MusicStoreContext _context;

        public DetailsModel(SE1611_Group3_A3.Models.MusicStoreContext context)
        {
            _context = context;
        }

      public Album Album { get; set; }
        public string artistName;
        public string genreName;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FirstOrDefaultAsync(m => m.AlbumId == id);
            
            if (album == null)
            {
                return NotFound();
            }
            else 
            {
                Album = album;

                var artist = await _context.Artists.FirstOrDefaultAsync(m => m.ArtistId == album.ArtistId);
                var genre = await _context.Genres.FirstOrDefaultAsync(m => m.GenreId == album.GenreId);
                artistName = artist.Name;
                genreName = genre.Name;
                
            }
            return Page();
        }
    }
}
