using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Id3v1TagAdjuster
{
    public class Artist
    {
        public Artist(string path)
        {
            FullPath = path;

            Name = new DirectoryInfo(path).Name;

            Albums = new List<Album>();

            LoadAlbums();
        }

        public string Name { get; set; }

        public List<Album> Albums { get; set; }

        public string FullPath { get; set; }

        public void AddAlbum(string albumPath)
        {
            Albums.Add(new Album(this, albumPath));
        }

        public int CountTotalTracks()
        {
            return Albums.Sum(x => x.Tracks.Count);
        }

        private void LoadAlbums()
        {
            string[] arrDir = Directory.GetDirectories(FullPath);

            foreach (string dir in arrDir)
                AddAlbum(dir);
        }
    }
}
