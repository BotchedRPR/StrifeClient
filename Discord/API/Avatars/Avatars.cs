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
