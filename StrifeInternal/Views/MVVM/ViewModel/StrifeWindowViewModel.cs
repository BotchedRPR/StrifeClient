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
        public void GetChannelMessages()
        {
            var task = Task.Run(() => Discord.API.Channels.Channels.GetChannelMessages(Discord.APIUris.GetChannelUri(_dmid)));
            dynamic json = Discord.API.Channels.Channels.ParseJsonDataFromRequest(task.Result.ToString());
            for (int i = json.Count - 1; i > -1; i--)
            { 
                Messages.Add(new MessageModel
                {
                    Username = json[i].author.username,
                    ImageSource = Discord.API.Avatars.Avatars.GetAvatarUri(json[i].author.id.ToString(), json[i].author.avatar.ToString()),
                    Message = json[i].content,
                    MessageSent = json[i].timestamp,
                    IsNativeOrigin = false,
                    IsFirstMessage = true
                });
            }
            var _task = Task.Run(() => Discord.API.DMs.DMs.GetUserDMs());
            json = Discord.API.Channels.Channels.ParseJsonDataFromRequest(_task.Result.ToString());
        }
        public StrifeWindowViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            GetChannelFromApi = new RelayCommand(GetChannelMessages);
        }
    }
}
