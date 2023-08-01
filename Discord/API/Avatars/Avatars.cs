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

namespace StrifeClient.Discord.API.Avatars
{
    class Avatars
    {
        public static string GetAvatarUri(string userID, string avatarID)
        {
            if (avatarID.StartsWith("a_"))
                avatarID = avatarID.Remove(0,2);
            string fullUrl = "https://cdn.discordapp.com/avatars/" + userID + "/a_" + avatarID + ".webp";
            return(fullUrl);
        }
    }
}
