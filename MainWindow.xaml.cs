﻿/*
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
using StrifeClient.Discord;
using StrifeClient.StrifeInternal;
using StrifeClient.StrifeInternal.TokenSecurity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StrifeClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient client = new();
        bool isAuth = false;
        public MainWindow()
        {
            Logger.InitLogger();
            Logger.Log("All initialization done, calling InitializeComponent()", Logger.LogLevel.Debug);
            // If we store a token - encrypted or not, we don't need to call InitializeComponent as we dont need to show MainWindow.
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.enc") || System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw"))
            {
                PasswordEntry pen = new();
                pen.Show();
                this.Close();
            }
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isAuth = false;
            Logger.Log("Setting headers.", Logger.LogLevel.Debug);
            if (!client.DefaultRequestHeaders.Contains("Authorization"))
            {
                Logger.Log("Setting Authorization...", Logger.LogLevel.Debug);
                client.DefaultRequestHeaders.Add("Authorization", token.Text);
            }
            else
            {
                Logger.Log("We already have Authorization set. Re-setting...", Logger.LogLevel.Warning);
                client.DefaultRequestHeaders.Remove("Authorization");
                Logger.Log("Setting Authorization...", Logger.LogLevel.Debug);
                client.DefaultRequestHeaders.Add("Authorization", token.Text);
            }
            Logger.Log("Checking token...", Logger.LogLevel.Debug);
            var check = Authentication.CheckAuthorizationHeader();
            if (!check)
            {
                Logger.Log("Token check failed.", Logger.LogLevel.Error);
            }
            else
            {
                Logger.Log("Token is correct.", Logger.LogLevel.Success);
                if (MessageBox.Show("Would you like to save your Discord token?", "Strife - question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    SaveToken(token.Text);
                isAuth = true;
            }
            this.Close();
        }

        private static void SaveToken(string token)
        {
            if (System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient"))
            {
                Logger.Log("StrifeClient directory already exists? Removing.", Logger.LogLevel.Warning);
                System.IO.Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient", true);
            }
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient");
            if (MessageBox.Show("Would you like to encrypt your token using a password?", "Strife - question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TokenSecurity.SaveTokenWithPassword(token);
            }
            else
                TokenSecurity.SaveTokenInsecurely(token);
            Logger.Log("Alright, token saved. Quitting MainWindow and going into PasswordEntry.", Logger.LogLevel.Debug);
            PasswordEntry pen = new();
            pen.Show();
        }
    }
}
