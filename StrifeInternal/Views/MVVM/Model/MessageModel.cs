using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrifeClient.StrifeInternal.Views.MVVM.Model
{
    class MessageModel
    {
        public string Username { get; set; }
        public string ImageSource { get; set; }
        public string Message { get; set; }
        public DateTime MessageSent { get; set; }
        public bool IsNativeOrigin { get; set; }
        public bool ?IsFirstMessage { get; set; }
    }
}
