using Newtonsoft.Json;
using StrifeClient.StrifeInternal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrifeClient.Discord.API.Channels
{
    class Channels
    {
        public async static Task<String>GetChannelMessages(Uri channelUri)
        {
            var request = await MainWindow.client.GetAsync(channelUri);
            if (request.IsSuccessStatusCode)
                return await request.Content.ReadAsStringAsync();
            else
                Logger.Log("Something went wrong with DiscordAPI, requested Uri returned: \n" + request.ToString(), Logger.LogLevel.Error);
            return "An error has occured";
        }
        public static dynamic ParseJsonDataFromRequest(string request)
        {
            return JsonConvert.DeserializeObject<dynamic>(request); //we use dynamic types in case discord changes something inside of their api
        }
    }
}
