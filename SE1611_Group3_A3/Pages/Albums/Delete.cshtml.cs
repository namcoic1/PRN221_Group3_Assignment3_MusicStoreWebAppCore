using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.Pages.Albums
{
    public class DeleteModel : PageModel
    {
        private readonly SE1611_Group3_A3.Models.MusicStoreContext _context;

        public DeleteModel(SE1611_Group3_A3.Models.MusicStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Album Album { get; set; }
        [BindProperty]
        public Artist Artist { get; set; }
        [BindProperty]
        public Genre Genre { get; set; }



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
                if(artist != null && genre != null)
                {
                    Artist = artist;
                    Genre= genre;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }
            var album = await _context.Albums.FindAsync(id);

            if (album != null)
            {
                Album = album;
                _context.Albums.Remove(Album);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
