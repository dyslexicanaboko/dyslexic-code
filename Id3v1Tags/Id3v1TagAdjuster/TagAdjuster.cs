using TagLib;

namespace Id3v1TagAdjuster
{
    public class TagAdjuster
    {
        public void FixTags(Artist artist)
        {
            File f = null;

            var arrArtists = new string[] { artist.Name };
            var arrGenres = new string[] { "Metal", "Punk Rock" };

            foreach (Album a in artist.Albums)
            {
                foreach (Track t in a.Tracks)
                {
                    f = File.Create(t.FullPath);
                    f.Tag.Artists = arrArtists; //This is deprecated, but I don't know how else to fix it
                    f.Tag.AlbumArtists = arrArtists;
                    f.Tag.Year = (uint)a.Year;
                    f.Tag.Album = a.Name;
                    f.Tag.Track = (uint)t.TrackNumber;
                    f.Tag.Title = t.Title;
                    f.Tag.Genres = arrGenres;
                    f.Save();
                }
            }
        }
    }
}
