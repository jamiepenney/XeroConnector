using System.IO;
using System.Security.Cryptography.X509Certificates;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using XeroConnector.Interfaces;

namespace XeroConnector
{
    public class XeroConnectionFactory
    {
        private readonly string _privateConsumerKey;
        private readonly string _privateConsumerSecret;
        private readonly string _privateUserAgentString;
        private readonly string _privateKeyFile;
        private readonly string _privateKeyPassword;

        public XeroConnectionFactory(string privateConsumerKey, string privateConsumerSecret, string privateUserAgentString, string privateKeyFile, string privateKeyPassword)
        {
            _privateConsumerKey = privateConsumerKey;
            _privateConsumerSecret = privateConsumerSecret;
            _privateUserAgentString = privateUserAgentString;
            _privateKeyFile = privateKeyFile;
            _privateKeyPassword = privateKeyPassword;
        }

        public XeroConnectionFactory(string configFile)
        {
            using(var fileReader = new StreamReader(configFile))
            {
                _privateConsumerKey = fileReader.ReadLine() ?? "".Trim();
                _privateConsumerSecret = fileReader.ReadLine() ?? "".Trim();
                _privateUserAgentString = fileReader.ReadLine() ?? "".Trim();
                _privateKeyFile = fileReader.ReadLine() ?? "".Trim();
                _privateKeyPassword = fileReader.ReadLine() ?? "".Trim();
            }
        }

        public IOAuthSession CreatePrivateConsumerSession()
        {
            // Load the private certificate from disk using the password used to create it

            var privateCertificate = string.IsNullOrEmpty(_privateKeyPassword) ? 
                new X509Certificate2(_privateKeyFile) : 
                new X509Certificate2(_privateKeyFile, _privateKeyPassword);

            // Create the consumer session
            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = _privateConsumerKey,
                ConsumerSecret = _privateConsumerSecret,
                SignatureMethod = SignatureMethod.RsaSha1,
                UseHeaderForOAuthParameters = true,
                Key = privateCertificate.PrivateKey,
                UserAgent = _privateUserAgentString
            };

            return new OAuthSession(
                consumerContext,
                "https://api.xero.com/oauth/RequestToken",
                "https://api.xero.com/oauth/Authorize",
                "https://api.xero.com/oauth/AccessToken");
        }

        public IToken CreatePrivateAccessToken()
        {
            // Note: For private applications, the consumer key and secret are also used as the access token and secret
            return new TokenBase
            {
                Token = _privateConsumerKey,
                TokenSecret = _privateConsumerSecret
            };
        }

        public IXeroConnection CreateXeroConnection()
        {
            return new XeroConnection(CreatePrivateConsumerSession(), CreatePrivateAccessToken());
        }
    }
}
