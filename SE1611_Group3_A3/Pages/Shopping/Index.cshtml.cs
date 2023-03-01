using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE1611_Group3_A3.DataAccess;
using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.Pages.Shopping
{
    public class IndexModel : PageModel
    {
        AlbumLogic albumLogic = new AlbumLogic();
        GenreLogic genreLogic = new GenreLogic();

        public List<Genre> genres { get; set; }

        public void OnGet()
        {
            genres = genreLogic.GetGenres();
        }
    }
}
