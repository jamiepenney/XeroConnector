using System;

namespace XeroConnector.Sample
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connectionFactory = new XeroConnectionFactory("config.cfg");
            var session = new XeroSession(connectionFactory.CreateXeroConnection());

            var response = session.GetOrganisation();
            if(response.Status == "OK")
            {
                var org = response.Result;
                Console.WriteLine("Org name: {0}", org.Name);
            }
            else
            {
                Console.WriteLine("Error occurred: {0}", string.Join(",", response.Errors));
            }

            Console.ReadKey();
        }
    }
}
