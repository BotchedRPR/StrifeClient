using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        public static void LogToFile(string log)
        {
            System.IO.File.WriteAllText("latest.log", log);
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
        public static void Log(string log, LogLevel logLevel)
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
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(CreateFunctionHeader(className) + log);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(CreateFunctionHeader(className) + log);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(CreateFunctionHeader(className) + log);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            LogToFile(CreateFunctionHeader(className) + log);
        }
        public static void InitLogger()
        {
            Console.ForegroundColor = ConsoleColor.White;
            // initialize the main console first. we need some output if the main window does not load for whatever reason.
            Console.WriteLine("Hi, welcome to Strife.\nSetting up class: Logger");
            Log("Class setup done", LogLevel.Debug);
        }
    }
}
