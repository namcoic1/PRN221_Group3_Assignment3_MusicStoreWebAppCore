﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.Pages.Albums
{
    public class IndexModel : PageModel
    {
        private readonly SE1611_Group3_A3.Models.MusicStoreContext _context;

        public IndexModel(SE1611_Group3_A3.Models.MusicStoreContext context)
        {
            _context = context;
        }

        public IList<Album> Album { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Albums != null)
            {
                Album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre).ToListAsync();
            }
        }
    }
}
