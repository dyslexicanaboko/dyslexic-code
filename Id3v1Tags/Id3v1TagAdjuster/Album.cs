using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Id3v1TagAdjuster
{
    public class Album : IMusicMetaData<Artist>
    {
        public Album(Artist artistReference, string path)
        {
            ParentNode = artistReference;
            
            FullPath = path;

            Tracks = new List<Track>();

            LoadMetaDataFromPath(path);

            LoadTracks();
        }

        public void LoadMetaDataFromPath(string path)
        {
            string[] arr = new DirectoryInfo(path).Name.Split(' ');

            Year = Convert.ToInt32(arr[0]);

            Name = string.Join(" ", arr.Skip(1).ToArray());
        }

        public string GetArtistName()
        {
            return ParentNode.Name;
        }

        public string FullPath { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public Artist ParentNode { get; set; }

        public List<Track> Tracks { get; set; }

        public void AddTrack(string trackPath)
        {
            Tracks.Add(new Track(this, trackPath));
        }

        public void LoadTracks()
        {
            List<string> lst = Directory.EnumerateFiles(FullPath, "*.mp3", SearchOption.AllDirectories).ToList();

            foreach (string mp3 in lst)
                AddTrack(mp3);
        }
    }
}
