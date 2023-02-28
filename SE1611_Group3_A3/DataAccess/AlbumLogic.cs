using SE1611_Group3_A3.Models;

namespace SE1611_Group3_A3.DataAccess
{
    public class AlbumLogic
    {
        public MusicStoreContext _context { get; set; } = new MusicStoreContext();

        public List<Album> GetAlbums()
        {
            return _context.Albums.ToList();
        }

        public List<Album> GetAlbumsByGerneId(int gid)
        {
            return _context.Albums.Where(x=>x.GenreId == gid).ToList();
        }
    }
}
