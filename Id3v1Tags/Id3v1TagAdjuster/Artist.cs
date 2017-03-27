using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Id3v1TagAdjuster
{
    public class Artist
    {
        public Artist(string path, bool useWholeName)
        {
            FullPath = path;

            Name = new DirectoryInfo(path).Name;

            Albums = new List<Album>();

            LoadAlbums(useWholeName);
        }

        public string Name { get; set; }

        public List<Album> Albums { get; set; }

        public string FullPath { get; set; }

        public void AddAlbum(string albumPath, bool useWholeName)
        {
            Albums.Add(new Album(this, albumPath, useWholeName));
        }

        public int CountTotalTracks()
        {
            return Albums.Sum(x => x.Tracks.Count);
        }

        private void LoadAlbums(bool useWholeName)
        {
            string[] arrDir = Directory.GetDirectories(FullPath);

            foreach (string dir in arrDir)
                AddAlbum(dir, useWholeName);
        }
    }
}
