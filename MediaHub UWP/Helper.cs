
using TMDbLib.Client;

namespace MediaHub_UWP
{
    public static class Helper
    {
        private static readonly string API_KEY = "key here";

        public static TMDbClient CreateClient()
        {
            return new TMDbClient(API_KEY);
        }
    }
}
