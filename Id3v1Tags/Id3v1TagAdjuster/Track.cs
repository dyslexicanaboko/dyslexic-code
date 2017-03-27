using System;
using System.IO;

namespace Id3v1TagAdjuster
{
    public class Track : IMusicMetaData<Album>, TagLib.File.IFileAbstraction
    {
        public Track(Album albumReference, string path, bool useWholeName)
        {
            ParentNode = albumReference;

            FullPath = path;

            LoadMetaDataFromPath(path, useWholeName);
        }

        public string FullPath { get; set; }

        public int TrackNumber { get; set; }

        public string Name { get; set; }

        public string Title { get { return Name; } set { Name = value; } }

        public Album ParentNode { get; set; }

        public string GetArtistName()
        {
            return ParentNode.GetArtistName();
        }

        public string GetAlbumName()
        {
            return ParentNode.Name;
        }

        public void LoadMetaDataFromPath(string path, bool useWholeName)
        {
            string strName = new DirectoryInfo(path).Name;

            if (useWholeName)
                Title = strName;
            else
            {
                //This case is crappy because it assumes too much
                string[] arr = strName.Split(new string[] { " - " }, StringSplitOptions.None);

                TrackNumber = Convert.ToInt32(arr[0]);

                Title = arr[1].Replace(".mp3", string.Empty);
            }
        }

        private Stream _fileStream;

        public void CloseStream(Stream stream)
        {
            if(stream != null)
                stream.Close();
        }

        private Stream GetStream()
        { 
            if(_fileStream == null)
                _fileStream = new FileStream(FullPath, FileMode.Open);

            return _fileStream;
        }

        public Stream ReadStream
        {
            get { return GetStream(); }
        }

        public Stream WriteStream
        {
            get { return GetStream(); }
        }

        public string GetArtistAlbumTrackString(string trackId = null)
        {
            string strName = null;

            if (!string.IsNullOrWhiteSpace(trackId))
                strName = trackId + " " + Name;
            else
                strName = Name;

            return GetArtistName() + " # " + GetAlbumName() + " # " + strName;
        }
    }
}
