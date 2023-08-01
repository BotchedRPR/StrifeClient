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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StrifeClient.StrifeInternal
{
    internal class Logger
    {
        public enum LogLevel
        { 
            Success = 0,
            Debug = 1,
            Error = 2,
            Warning = 3,
            Fatal = 4
        }
        private static void LogToFile(string log)
        {
            System.IO.File.AppendAllText("latest.log", "\n" + log);
        }
        public static string GetCurrentDateAndTime()
        {
            DateTime LogTime = DateTime.Now;
            return LogTime.ToString();
        }
        public static string CreateFunctionHeader(string callerName)
        {
            return "[" + GetCurrentDateAndTime() + "]" + " " + callerName + " - ";
        }
        public static void Log(string log, LogLevel logLevel = LogLevel.Debug)
        {
#pragma warning disable CS8602
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;
#pragma warning restore CS8602
            switch (logLevel)
            { 
                case LogLevel.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(CreateFunctionHeader(className) + log);
                    Console.ForegroundColor = ConsoleColor.White;
                    LogToFile(CreateFunctionHeader(className) + "Success - " + log);
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(CreateFunctionHeader(className) + log);
                    Console.ForegroundColor = ConsoleColor.White;
                    LogToFile(CreateFunctionHeader(className) + "Debug - " + log);
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(CreateFunctionHeader(className) + log);
                    Console.ForegroundColor = ConsoleColor.White;
                    LogToFile(CreateFunctionHeader(className) + "ERROR - " + log);
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(CreateFunctionHeader(className) + log);
                    Console.ForegroundColor = ConsoleColor.White;
                    LogToFile(CreateFunctionHeader(className) + "Warning - " + log);
                    break;
                case LogLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(CreateFunctionHeader(className) + log + " This error is FATAL.");
                    LogToFile(CreateFunctionHeader(className) + "FATAL! - " + log);
                    MessageBox.Show("The application has encountered an unrecoverable error. More info can be found in the latest.log file.\nStrife will now close.", "Strafe - fatal", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                    return;
            }
        }
        public static void InitLogger()
        {
            Console.ForegroundColor = ConsoleColor.White;
            // initialize the main console first. we need some output if the main window does not load for whatever reason.
            Console.WriteLine("Hi, welcome to Strife.\nSetting up class: Logger");
            System.IO.File.AppendAllText("latest.log", "Log started at " + GetCurrentDateAndTime());
            Logger.Log("Copyright (C) 2023  BotchedRPR\r\n\r\n    This program is free software: you can redistribute it and/or modify\r\n    it under the terms of the GNU General Public License as published by\r\n    the Free Software Foundation, either version 3 of the License, or\r\n    (at your option) any later version.\r\n\r\n    This program is distributed in the hope that it will be useful,\r\n    but WITHOUT ANY WARRANTY; without even the implied warranty of\r\n    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\r\n    GNU General Public License for more details.\r\n\r\n    You should have received a copy of the GNU General Public License\r\n    along with this program.  If not, see <https://www.gnu.org/licenses/>.", LogLevel.Debug);
            Log("Class setup done", LogLevel.Debug);
        }
    }
}
