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
        public static string GetCurrentDateAndTime()
        {
            DateTime LogTime = DateTime.Now;
            return LogTime.ToString();
        }
        public static string CreateFunctionHeader(string callerName)
        {
            return "[" + GetCurrentDateAndTime() + "]" + " " + callerName + " - ";
        }
        public static void Log(string log)
        {
            Console.WriteLine("[WARNING] Do not use the Log() method. Use a LogLevel.\nPRs that use Log() will be automatically rejected.");
            LogDebug(log);
        }
        public static void LogWarning(string log)
        {
#pragma warning disable CS8602
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;
#pragma warning restore CS8602
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(CreateFunctionHeader(className) + log);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void LogError(string log)
        {
#pragma warning disable CS8602
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;
#pragma warning restore CS8602
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(CreateFunctionHeader(className) + log);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void LogDebug(string log)
        {
#pragma warning disable CS8602
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;
#pragma warning restore CS8602
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(CreateFunctionHeader(className) + log);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void LogSuccess(string log)
        {
#pragma warning disable CS8602
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;
#pragma warning restore CS8602
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CreateFunctionHeader(className) + log);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void InitLogger()
        {
            Console.ForegroundColor = ConsoleColor.White;
            // initialize the main console first. we need some output if the main window does not load for whatever reason.
            Console.WriteLine("Hi, welcome to Strife.\nSetting up class: Logger");
            LogDebug("Class setup done");
        }
    }
}
