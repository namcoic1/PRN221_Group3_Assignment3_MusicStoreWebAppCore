using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.DataAccess
{
    public class GenreLogic
    {
        public MusicStoreContext _context { get; set; } = new MusicStoreContext();

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

    }
}
