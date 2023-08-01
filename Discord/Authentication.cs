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
using StrifeClient.StrifeInternal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrifeClient.Discord
{
    internal class Authentication
    {
        public static bool CheckAuthorizationHeader()
        {
            if (!MainWindow.client.DefaultRequestHeaders.Contains("Authorization"))
            {
                Logger.Log("Authorization header NOT found!", Logger.LogLevel.Error);
            }
            else
            {
                var check = MainWindow.client.GetAsync(APIUris.AuthenticationCheck);
                if (check.Result.IsSuccessStatusCode)
                    return true;
            }
            return false;
        }
    }
}
