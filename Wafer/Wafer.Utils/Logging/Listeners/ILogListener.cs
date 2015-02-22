using System;

namespace Wafer.Utils.Logging.Listeners {
    public interface ILogListener {
        void MessageReceived(LogType type, string tag, string message);
    }
}
