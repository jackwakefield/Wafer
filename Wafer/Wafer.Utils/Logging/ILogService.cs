using System;
using Wafer.Utils.Logging.Listeners;

namespace Wafer.Utils.Logging {
    public interface ILogService {
        void AddListener(ILogListener listener);
        void RemoveListener(ILogListener listener);
        void Verbose(string tag, string message, params object[] parameters);
        void Debug(string tag, string message, params object[] parameters);
        void Info(string tag, string message, params object[] parameters);
        void Warn(string tag, string message, params object[] parameters);
        void Error(string tag, string message, params object[] parameters);
    }
}

