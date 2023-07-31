using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StrifeClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // last resort - if deleting this failed when setting token, try again when we exit. we do not want to leave raw token here.
            System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\StrifeClient\token.raw");
        }
    }
}
