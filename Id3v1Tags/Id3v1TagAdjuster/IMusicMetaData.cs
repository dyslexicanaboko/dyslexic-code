
namespace Id3v1TagAdjuster
{
    public interface IMusicMetaData<P>
    {
        string FullPath { get; set; }
        
        string Name { get; set; }
        
        P ParentNode { get; set; }
        
        void LoadMetaDataFromPath(string path, bool useWholeName);
    }
}
