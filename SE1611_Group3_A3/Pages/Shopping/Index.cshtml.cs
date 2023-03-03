using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.Pages.Shopping
{
    public class IndexModel : PageModel
    {
        private readonly MusicStoreContext _context;
        private readonly IConfiguration Configuration;
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; }
        public int GenreId { get; set; } = -1;
        public int TotalPage { get; set; }
        public IList<Genre> genres { get; set; } = new List<Genre>();
        public IList<Artist> artists { get; set; } = new List<Artist>();
        public PaginatedList<Album> albums { get; set; }

        public IndexModel(MusicStoreContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task OnGetAsync(int? pageIndex, int genreId, string search)
        {
            genres = _context.Genres.ToList();
            artists = _context.Artists.ToList();

            Search = search;
            GenreId = genreId;

            IQueryable<Album> albumsIQ = from s in _context.Albums
                                             select s;
            if(genreId != 0)
            {
                albumsIQ = albumsIQ.Where(album => (album.GenreId == genreId || GenreId == -1));
            }

            if (!String.IsNullOrEmpty(search))
            {
                albumsIQ = albumsIQ.Where(album => album.Title.Contains(search));
            }

            var pageSize = Configuration.GetValue("PageSize", 0);
            TotalPage = (int)Math.Ceiling(albumsIQ.Count() / (double)pageSize); ;
            PageIndex = pageIndex ?? 1;

            albums = await PaginatedList<Album>.CreateAsync(
                albumsIQ.AsNoTracking(), PageIndex, pageSize);
        }
    }
}
