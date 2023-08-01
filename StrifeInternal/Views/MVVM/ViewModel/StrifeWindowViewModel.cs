using StrifeClient.StrifeInternal.Views.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace StrifeClient.StrifeInternal.Views.MVVM.ViewModel
{
    class StrifeWindowViewModel : Observable.ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public string _dmid;
        public string DMID 
        {
            get { return _dmid; }
            set { _dmid = value; }
        }
        public ICommand GetChannelFromApi { get; private set; }
        public void test()
        {
            Logger.Log("Hello from mvvm buttonm, uri is " + Discord.APIUris.GetChannelUri(_dmid), Logger.LogLevel.Debug);
        }
        public StrifeWindowViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            GetChannelFromApi = new RelayCommand(test);

            Messages.Add(new MessageModel
            {
                Username = "Dummy",
                ImageSource = "nah",
                Message = "Hello",
                MessageSent = DateTime.Now,
                IsNativeOrigin = false,
                IsFirstMessage = true
            });
            Messages.Add(new MessageModel
            {
                Username = "Dummy",
                ImageSource = "nah",
                Message = "Hello",
                MessageSent = DateTime.Now,
                IsNativeOrigin = false,
                IsFirstMessage = false
            });
            Messages.Add(new MessageModel
            {
                Username = "Dummy",
                ImageSource = "nah",
                Message = "Hello",
                MessageSent = DateTime.Now,
                IsNativeOrigin = false,
                IsFirstMessage = false
            });
        }
    }
}
