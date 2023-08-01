using StrifeClient.StrifeInternal.Views.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrifeClient.StrifeInternal.Views.MVVM.ViewModel
{
    class StrifeWindowViewModel : Observable.ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public ContactModel SelectedContact { get; set; }



        public StrifeWindowViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

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
