using System.Collections.Generic;
using System.Security.Cryptography;

namespace MediaHub_UWP
{
    public static class Extensions
    {
        public static List<T> Shuffle<T>(this List<T> list)
        {
            // https://stackoverflow.com/a/1262619
            var provider = new RNGCryptoServiceProvider();

            int n = list.Count;

            while (n > 1)
            {
                n--;
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
