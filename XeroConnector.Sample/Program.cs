using System;

namespace XeroConnector.Sample
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connectionFactory = new XeroConnectionFactory("config.cfg");
            var session = new XeroSession(connectionFactory.CreateXeroConnection());

            var org = session.GetOrganisation();
            Console.WriteLine("Org name: {0}", org.Name);

            Console.ReadKey();
        }
    }
}
