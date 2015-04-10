using System;

namespace XDBookmarkTools.Controllers
{
    public class Logger
    {
        public event MessageEventHandler MessageHandler;

        public delegate void MessageEventHandler(Logger sender, LogEventArgs e);

        public void LogMsg(string message, LogEventArgs.MessageType messageType = LogEventArgs.MessageType.Info)
        {
            var eargs = new LogEventArgs {Message = message, MType = messageType};
            if (MessageHandler != null) MessageHandler(this, eargs);
        }

        public void LogInfo(string message)
        {
            LogMsg(message);
        }

        public void LogError(string message)
        {
            LogMsg(message, LogEventArgs.MessageType.Error);
        }

        public class LogEventArgs : EventArgs
        {
            public enum MessageType
            {
                Info,
                Warning,
                Error,
                Critical
            };

            public MessageType MType = MessageType.Info;
            public string Message = "";

            public string LevelText
            {
                get
                {
                    switch (MType)
                    {
                        case MessageType.Critical:
                            return "CRITICAL";
                        case MessageType.Info:
                            return "INFO";
                        case MessageType.Error:
                            return "ERROR";
                        case MessageType.Warning:
                            return "WARNING";
                        default:
                            return "UNDEF_ERR";
                    }
                }
            }

            public override string ToString()
            {
                return DateTime.Now.ToString("yy/MM/dd-HH:mm:ss") + " - " + LevelText + " : " + Message +
                       Environment.NewLine;
            }
        }
    }
}