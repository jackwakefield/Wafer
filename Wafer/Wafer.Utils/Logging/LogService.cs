using System;
using System.Collections.Generic;
using System.Diagnostics;
using Wafer.Utils.Logging.Listeners;

namespace Wafer.Utils.Logging {
    public class LogService : ILogService {
        private readonly List<ILogListener> listeners;

        public LogService() {
            listeners = new List<ILogListener>();
        }

        public void AddListener(ILogListener listener) {
            if (!listeners.Contains(listener)) {
                listeners.Add(listener);
            }
        }

        public void RemoveListener(ILogListener listener) {
            if (listeners.Contains(listener)) {
                listeners.Remove(listener);
            }
        }

        public void Verbose(string tag, string message, params object[] parameters) {
            DisptchMessage(LogType.Verbose, tag, string.Format(message, parameters));
        }

        public void Debug(string tag, string message, params object[] parameters) {
            DisptchMessage(LogType.Debug, tag, string.Format(message, parameters));
        }

        public void Info(string tag, string message, params object[] parameters) {
            DisptchMessage(LogType.Info, tag, string.Format(message, parameters));
        }

        public void Warn(string tag, string message, params object[] parameters) {
            DisptchMessage(LogType.Warn, tag, string.Format(message, parameters));
        }

        public void Error(string tag, string message, params object[] parameters) {
            DisptchMessage(LogType.Error, tag, string.Format(message, parameters));
        }

        private void DisptchMessage(LogType type, string tag, string message) {
            listeners.ForEach(l => l.MessageReceived(type, tag, message));
        }
    }
}
