using NUnit.Framework;

namespace Id3v1TagAdjuster.NUnit.Tests
{
    [TestFixture]
    public class TagAdjusterTests
    {
        [Test, Ignore]
        [TestCase(@"F:\Dump\ID3v1 tag test\Maximum The Hormone - マキシマム ザ ホルモン", 1, 7)]
        public void TargetDirectoryContains_X_Mp3s(string directory, int albums, int tracks)
        { 
            //Given that I have a target album with MP3s in it

            //When I query the target directory
            var obj = new TagAdjuster();

            var a = new Artist(directory, false);

            obj.FixTags(a);

            //Then it has an expected number of tracks in it
            Assert.IsTrue(a.Albums.Count == albums);
            Assert.IsTrue(a.Albums[0].Tracks.Count == tracks);
        }

        [Test]
        [TestCase(@"G:\CD Data\Music\Maximum The Hormone - マキシマム ザ ホルモン", 6, 61)]
        public void FixTracks(string path, int numberOfAlbums, int totalTracks)
        {
            //Given that I have a target Artist

            //When I look at the artist's directory
            var a = new Artist(path, false);

            new TagAdjuster().FixTags(a);

            //Then I should get a count of albums
            Assert.IsTrue(a.Albums.Count == numberOfAlbums);
            Assert.IsTrue(a.CountTotalTracks() == totalTracks);
        }

        [Test, Ignore]
        [TestCase(@"G:\CD Data\Music\Maximum The Hormone - マキシマム ザ ホルモン", 6, 61)]
        public void ArtistObjectIsLoadedMultipleAlbumsAndTracks(string path, int numberOfAlbums, int totalTracks)
        {
            //Given that I have a target Artist

            //When I look at the artist's directory
            var a = new Artist(path, false);

            //Then I should get a count of albums
            Assert.IsTrue(a.Albums.Count == numberOfAlbums);
            Assert.IsTrue(a.CountTotalTracks() == totalTracks);
        }

        [Test, Ignore]
        [TestCase(@"G:\CD Data\Music\Maximum The Hormone - マキシマム ザ ホルモン", 6)]
        public void ArtistObjectIsLoadedWithNameAndAlbumNames(string path, int numberOfAlbums)
        { 
            //Given that I have a target Artist

            //When I look at the artist's directory
            var obj = new TagAdjuster();
            var a = new Artist(path, false);

            //Then I should get a count of albums
            Assert.IsTrue(a.Albums.Count == numberOfAlbums);
        }

        [Test, Ignore]
        [TestCase(@"G:\CD Data\Music\Maximum The Hormone - マキシマム ザ ホルモン\2001 鳳 - Ootori", 2001, "鳳 - Ootori", 7)]
        public void AlbumIsLoadedWithTracks(string path, int year, string name, int numberOfTracks)
        {
            //Given that I have a target Artist

            //When I look at the artist's directory
            var obj = new TagAdjuster();
            var a = new Album(null, path, false);

            //Then I should get a count of albums
            Assert.IsTrue(a.Year == year);
            Assert.IsTrue(a.Name == name);
            Assert.IsTrue(a.Tracks.Count == numberOfTracks);
        }
    }
}
