using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Id3v1TagAdjuster.NUnit.Tests
{
    [TestFixture]
    public class FileTransformerTests
    {
        [Test, Ignore]
        [TestCase(@"H:\Test\Repo\", 1, 10)]
        public void Repo_directory_has_X_albums_with_Y_tracks(string repositoryPath, int albums, int tracks)
        {
            //Arrange
            var ft = new FileTransformer(repositoryPath);

            //Act
            ft.LoadAllArtists();

            var a = ft.Artists[0];

            //Assert
            Assert.IsTrue(a.Albums.Count == albums);
            Assert.IsTrue(a.Albums[0].Tracks.Count == tracks);
        }

        [Test, Ignore]
        [TestCase(@"G:\MP3 Test\Music\", 4, 33)]
        public void Directory_has_one_artist_X_albums_Y_tracks(string repositoryPath, int albums, int transformations)
        {
            //Arrange
            var lstArtists = new List<string>();
            var lstAlbums = new List<string>();
            var lstTracks = new List<string>();
            List<string> lst = null;

            //Act
            lst = Directory.EnumerateFiles(repositoryPath, "*.mp3", SearchOption.AllDirectories).ToList();

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

                if (!lstArtists.Contains(strArtistPath))
                    lstArtists.Add(strArtistPath);
            }

            Assert.IsTrue(lstArtists.Count == 1);

            var ft = new FileTransformer(lstArtists[0]);

            //Act
            ft.LoadAllArtists();

            //Assert
            Assert.IsTrue(ft.Artists[0].Albums.Count == albums);
            Assert.IsTrue(ft.Artists[0].Albums.Sum(x => x.Tracks.Count) == transformations);
        }

        [Test, Ignore]
        [TestCase(@"G:\MP3 Test\Music\", 4, 33)]
        public void Repo_directory_that_has_X_albums_contains_Y_transformations(string repositoryPath, int albums, int transformations)
        {
            //Arrange
            var ft = new FileTransformer(repositoryPath);

            //Act
            ft.LoadAllArtists();

            //Assert
            Assert.IsTrue(ft.Artists.Count == 1);
            Assert.IsTrue(ft.Artists[0].Albums.Count == albums);
            Assert.IsTrue(ft.Artists[0].Albums.Sum(x => x.Tracks.Count) == transformations);
        }

        [Test, Ignore]
        //[TestCase(@"H:\Test\Repo\", @"H:\Test")] //Test
        [TestCase(@"H:\Music\", @"H:\Music\")] //Actual
        public void Repo_directory_that_has_one_album_has_all_tracks_moved(string repositoryPath, string targetMovePath)
        {
            //Arrange
            int result = 0;
            var ft = new FileTransformer(repositoryPath);
            List<TrackMover> lst = null;

            //Act
            ft.LoadAllArtists();

            lst = ft.TransformTrackNames();

            ft.MoveAllTracks(targetMovePath);

            result = lst.Count(x => !x.IsMoved);

            //Assert
            Assert.IsTrue(result == 0);
        }

        [Test, Ignore]
        //[TestCase(@"H:\Test\Repo\", @"H:\Test")] //Test
        [TestCase(@"G:\MP3 Test\Music\", @"G:\MP3 Test\Transformed\")] //Actual
        public void Repo_directory_that_has_one_artist_four_albums_has_all_tracks_moved(string repositoryPath, string targetMovePath)
        {
            //Arrange
            int result = 0;
            var ft = new FileTransformer(repositoryPath);
            List<TrackMover> lst = null;

            //Act
            ft.LoadAllArtists();

            lst = ft.TransformTrackNames();

            ft.MoveAllTracks(targetMovePath);

            result = lst.Count(x => !x.IsMoved);

            //Assert
            Assert.IsTrue(result == 0);
        }

        [Test]
        //[TestCase(@"H:\Test\Repo\", @"H:\Test")] //Test
        [TestCase(@"G:\MP3 Test\Music\", @"G:\MP3 Test\Transformed\")] //Actual
        public void Repo_directory_that_has_one_artist_four_albums_has_all_tracks_moved(string repositoryPath, string targetMovePath)
        {
            //Arrange
            int result = 0;
            var ft = new FileTransformer(repositoryPath);
            List<TrackMover> lst = null;

            //Act
            ft.LoadAllArtists();

            lst = ft.TransformTrackNames();

            ft.CopyAllTracks(targetMovePath);

            result = lst.Count(x => !x.IsCopied);

            //Assert
            Assert.IsTrue(result == 0);
        }
    }
}
