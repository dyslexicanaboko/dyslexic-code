using System.ServiceModel.Channels;
using System.Xml;

namespace WCFServicesSecurity
{
    public class AuthorizationHeader : MessageHeader
    {
        public string AuthorizationToken { get; set; }

        public AuthorizationHeader(string authorizationToken)
        {
            AuthorizationToken = authorizationToken;
        }

        public override string Name
        {
            get { return Config.HeaderName; }
        }

        public override string Namespace
        {
            get { return Config.HeaderNamespace; }
        }

        protected override void OnWriteHeaderContents(System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            // Write the content of the header directly using the XmlDictionaryWriter
            writer.WriteElementString(Config.AuthorizationTokenName, AuthorizationToken);
        }

        public static AuthorizationHeader ReadHeader(XmlDictionaryReader reader)
        {
            AuthorizationHeader header = null;

            // Read the header content (key) using the XmlDictionaryReader
            if (reader.ReadToDescendant(Config.AuthorizationTokenName, Config.HeaderNamespace))
                header = new AuthorizationHeader(reader.ReadElementString());

            return header;
        }

        public bool IsAuthorized()
        {
            return AuthorizationToken == Config.AuthorizationTokenValue;
        }
    }
}
