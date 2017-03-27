using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Id3v1TagAdjuster
{
    public class FileTransformer
    {
        private string RepositoryPath { get; set; }
        public List<Artist> Artists { get; private set; }
        public List<TrackMover> TrackTransformations { get; private set; }

        public FileTransformer(string repositoryPath)
        {
            Artists = new List<Artist>();
            TrackTransformations = new List<TrackMover>();
            RepositoryPath = repositoryPath;
        }

        public void LoadAllArtists()
        {
            //Arrange
            var lstArtists = new List<string>();
            var lstAlbums = new List<string>();
            var lstTracks = new List<string>();
            List<string> lst = null;

            //Act
            lst = Directory.EnumerateFiles(RepositoryPath, "*.mp3", SearchOption.AllDirectories).ToList();

            string strPathOnly = null;
            string strArtistPath = null;
            string[] arr = null;

            foreach (string fullPath in lst)
            {
                //Get just the path without the file name
                //In : F:\Music\Metal\Iron Maiden\1980 - Iron Maiden\someMp3.mp3
                //Out: F:\Music\Metal\Iron Maiden\1980 - Iron Maiden\someMp3.mp3
                strPathOnly = Path.GetDirectoryName(fullPath);

                //Break it down into an array split it by directory
                //In : F:\Music\Metal\Iron Maiden\1980 - Iron Maiden\
                //Out: F:,Music,Metal,Iron Maiden,1980 - Iron Maiden
                arr = strPathOnly.Split('\\');

                //Recombine the array as a path again, but drop off the last directory
                //In : F:,Music,Metal,Iron Maiden,1980 - Iron Maiden
                //Out: F:\Music\Metal\Iron Maiden\
                strArtistPath = string.Join("\\", arr.Take(arr.Length - 1));

                if (!Artists.Exists(x => x.FullPath == strArtistPath))
                    Artists.Add(new Artist(strArtistPath, true));
            }
        }

        public List<TrackMover> TransformTrackNames()
        {
            //Get a huge f-ing list of tracks for every artist and every album
            List<Track> lst = 
                Artists.SelectMany(x => x.Albums)
                       .SelectMany(x => x.Tracks)
                       .ToList();

            foreach (Track t in lst)
                TrackTransformations.Add(new TrackMover(t));

            return TrackTransformations;
        }

        public void MoveAllTracks(string targetPath)
        {
            foreach (TrackMover tm in TrackTransformations)
                tm.Move(targetPath);
        }

        public void CopyAllTracks(string targetPath)
        {
            foreach (TrackMover tm in TrackTransformations)
                tm.Copy(targetPath);
        }
    }
}
