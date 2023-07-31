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
                    //OPEN MAIN WINDOW AS AN AUTHENTICATED USER HERE!
                    return;
                }
                Logger.Log("Token read succesfully!", Logger.LogLevel.Success);
            }
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                TokenSecurity.DecryptFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.enc", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw", TokenSecurity.GetSHA512(pbox.Password), TokenSecurity.salt, TokenSecurity.iterations); 
            }
            catch(Exception ex)
            {
                Logger.Log("Invalid password. " + ex, Logger.LogLevel.Error);
            }
            System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw");
        }
    }
}
