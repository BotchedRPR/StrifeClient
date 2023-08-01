using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrifeClient.Discord
{
    internal class APIUris
    {
        public static Uri AuthenticationCheck = new("https://discord.com/api/v9/users/@me/library?country_code=US");
        public static string ChannelBase = new("https://discord.com/api/v9/channels/");
        public static string ChannelPost = new("/messages?limit=50"); //replicate original client functionality, we should probably bind it to a view property but oh well.
        public static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
        public static Uri GetChannelUri(string ID)
        {
            return new Uri(Combine(Combine(ChannelBase, ID), ChannelPost));
        }

    }
}
