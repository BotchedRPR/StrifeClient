using StrifeClient.StrifeInternal.Views.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

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
            Logger.Log("Hello from mvvm buttonm, waiting for request result... ", Logger.LogLevel.Debug);
            var task = Task.Run(() => Discord.API.Channels.Channels.GetChannelMessages(Discord.APIUris.GetChannelUri(_dmid)));
            dynamic json = Discord.API.Channels.Channels.ParseJsonDataFromRequest(task.Result.ToString());
            for(int i = json.Count - 1; i > -1; i--)
            { 
                Messages.Add(new MessageModel
                {
                    Username = json[i].author.username,
                    ImageSource = json[i].author.avatar,
                    Message = json[i].content,
                    MessageSent = DateTime.Now,
                    IsNativeOrigin = false,
                    IsFirstMessage = true
                });
            }
        }
        public StrifeWindowViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            GetChannelFromApi = new RelayCommand(test);
        }
    }
}
