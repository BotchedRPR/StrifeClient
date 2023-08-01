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
        public static dynamic? ParseJsonDataFromRequest(string? request)
        {
            if (request != null)
                return JsonConvert.DeserializeObject<dynamic>(request); //we use dynamic types in case discord changes something inside of their api
            else
            {
                return "Request failure";
            }
        }
    }
}
