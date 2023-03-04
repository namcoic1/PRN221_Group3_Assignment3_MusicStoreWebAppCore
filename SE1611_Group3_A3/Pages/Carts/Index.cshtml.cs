using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.Pages.Carts
{
    public class IndexModel : PageModel
    {
        private readonly SE1611_Group3_A3.Models.MusicStoreContext _context;

        public IndexModel(SE1611_Group3_A3.Models.MusicStoreContext context)
        {
            _context = context;
        }

        public IList<Cart> Cart { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int del { get; set; }
        [BindProperty(SupportsGet = true)]
        public int add { get; set; }

        public async Task OnGetAsync()
        {
            if (del != 0)
            {
                Cart delCart = _context.Carts.FirstOrDefault(x => x.RecordId == del);
                if (delCart != null)
                {
                    if (delCart.Count > 1)
                    {
                        delCart.Count -= 1;
                    }
                    else
                    {
                        _context.Carts.Remove(delCart);
                    }
                    _context.SaveChanges();
                }
            }

            if (add != 0)
            {
                Cart delCart = _context.Carts.FirstOrDefault(x => x.RecordId == add);
                if (delCart != null)
                {
                    delCart.Count += 1;
                    _context.SaveChanges();
                }
            }

            if (_context.Carts != null)
            {
                Cart = await _context.Carts
                .Include(c => c.Album).ToListAsync();
            }
        }
    }
}
