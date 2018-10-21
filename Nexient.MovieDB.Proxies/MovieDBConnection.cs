using System.Configuration;
using TMDbLib.Client;

namespace Nexient.MovieDB
{
    internal static class MovieDBConnection
    {

        private static TMDbClient client = null;

        private static TMDbClient GetConnection()
        {
            string apikey = ConfigurationManager.AppSettings["MovieAPIKey"].ToString();
            return new TMDbClient(apikey);

        }

        public static TMDbClient database
        {
            get
            {
                if (client == null)
                {

                    client = GetConnection();
                }
                return client;
            }

        }

    }
}
