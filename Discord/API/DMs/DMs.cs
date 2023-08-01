using StrifeClient.StrifeInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrifeClient.Discord.API.DMs
{
    class DMs
    {
        public static async Task<dynamic> GetUserDMs()
        {
            var request = await MainWindow.client.GetAsync("https://discord.com/api/users/@me/channels");
            if (request.IsSuccessStatusCode)
                return await request.Content.ReadAsStringAsync();
            else
                Logger.Log("Something went wrong with DiscordAPI, requested Uri returned: \n" + request.ToString(), Logger.LogLevel.Error);
            return "An error has occured";
        }
    }
}
