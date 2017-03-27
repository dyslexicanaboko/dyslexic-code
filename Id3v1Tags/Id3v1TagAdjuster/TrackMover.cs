using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Id3v1TagAdjuster
{
    public class TrackMover
    {
        private Track _track;

        public string NewTrackName { get; private set; }

        public bool IsMoved { get; private set; }

        public bool IsCopied { get; private set; }

        public Exception Error { get; private set; }

        public TrackMover(Track track)
        {
            _track = track;
        }

        public bool Move(string targetPath)
        {
            IsMoved = FileOperation(targetPath, File.Move);

            return IsMoved;
        }

        public bool Copy(string targetPath)
        {
            IsCopied = FileOperation(targetPath, File.Copy);

            return IsCopied;
        }

        private bool FileOperation(string targetPath, Action<string, string> method)
        {
            bool fileOperationResult = false;

            try
            {
                //Initialize
                int i = 0;

                NewTrackName = _track.GetArtistAlbumTrackString();

                string strTarget = Path.Combine(targetPath, NewTrackName);

                while (File.Exists(strTarget))
                {
                    i++;

                    NewTrackName = _track.GetArtistAlbumTrackString("d" + i.ToString());

                    //Try again
                    strTarget = Path.Combine(targetPath, NewTrackName);
                }

                if (!File.Exists(_track.FullPath))
                    return false;

                method(_track.FullPath, strTarget);

                fileOperationResult = true;
            }
            catch (Exception ex)
            {
                Error = ex;

                fileOperationResult = false;
            }

            return fileOperationResult;
        }
    }
}
