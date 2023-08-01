using StrifeClient.Discord;
using StrifeClient.StrifeInternal.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StrifeClient.StrifeInternal.TokenSecurity
{
    /// <summary>
    /// Interaction logic for PasswordEntry.xaml
    /// </summary>
    public partial class PasswordEntry : Window
    {
        public string? token;
        public PasswordEntry()
        {
            // Now we need to check if the token that we want to authenticate with is decrypted. 
            if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.enc"))
            {
                Logger.Log("token.enc does not exist, let's assume that the token is NOT encrypted.", Logger.LogLevel.Warning);
                try
                {
                    token = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw");
                }
                catch (Exception ex)
                {
                    Logger.Log("Something went wrong while reading the Token. Exception: " + ex.Message, Logger.LogLevel.Error);
                    SetHeaders(token);
                    CheckToken();
                    StrifeWindow sw = new();
                    sw.Show();
                    this.Close();
                    return;
                }
                Logger.Log("Token read succesfully!", Logger.LogLevel.Success);
                SetHeaders(token);
            }
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw"); }
            catch { }
            try
            { 
                TokenSecurity.DecryptFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.enc", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw", TokenSecurity.GetSHA512(pbox.Password), TokenSecurity.salt, TokenSecurity.iterations);
                token = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw");
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw");
                SetHeaders(token);
                CheckToken();
                StrifeWindow sw = new();
                sw.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                Logger.Log("Invalid password. " + ex, Logger.LogLevel.Error);

            }
        }
        private void SetHeaders(string token)
        {
            Logger.Log("Setting headers.", Logger.LogLevel.Debug);
            if (!MainWindow.client.DefaultRequestHeaders.Contains("Authorization"))
            {
                Logger.Log("Setting Authorization...", Logger.LogLevel.Debug);
                MainWindow.client.DefaultRequestHeaders.Add("Authorization", token);
            }
            else
            {
                Logger.Log("We already have Authorization set. Re-setting...", Logger.LogLevel.Warning);
                MainWindow.client.DefaultRequestHeaders.Remove("Authorization");
                Logger.Log("Setting Authorization...", Logger.LogLevel.Debug);
                MainWindow.client.DefaultRequestHeaders.Add("Authorization", token);
            }
        }
        private void CheckToken() // This is void as if it cannot authenticate with discord we just close the app.
        {
            Logger.Log("Checking token...", Logger.LogLevel.Debug);
            var check = Authentication.CheckAuthorizationHeader();
            if (!check)
            {
                Logger.Log("Token check failed.", Logger.LogLevel.Fatal);
            }
            else
            {
                Logger.Log("Authenticated succesfully! Token is correct!", Logger.LogLevel.Success);
            }
        }

        private void pbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Button_Click(sender, e);
        }
    }
}
