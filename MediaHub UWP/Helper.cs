
using System.Collections.Generic;
using TMDbLib.Client;

namespace MediaHub_UWP
{
    public static class Helper
    {
        private static readonly string API_KEY = "12f5e73e17b4748571bc3feb276bf1e4";

        public static TMDbClient CreateClient()
        {
            return new TMDbClient(API_KEY);
        }
    }
}
