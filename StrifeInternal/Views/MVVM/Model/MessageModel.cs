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
