/*
	Copyright (c) 2023 BotchedRPR
	This program is free software; you can redistribute it and/or modify it
	under the terms of the GNU General Public License as published by the
	Free Software Foundation, version 3.

	This program is distributed in the hope that it will be useful, but 
	WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
	General Public License for more details.

	You should have received a copy of the GNU General Public License 
	along with this program. If not, see <http://www.gnu.org/licenses/>.
*/ 
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
