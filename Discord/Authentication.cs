﻿using StrifeClient.StrifeInternal;
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
