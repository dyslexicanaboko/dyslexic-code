
namespace F5EverywhereLib.Domain
{
    public class ServerNode
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Instance { get; set; }
        public string Version { get; set; }

        //You won't find a connection string here because that requires a database name
        //Look at your database
    }
}
