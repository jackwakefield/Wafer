using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.Utils.Native;

namespace Wafer.Utils.Logging.Listeners {
    public class ConsoleLogListener : ILogListener {
        public ConsoleLogListener() {
            CreateConsole();
        }

        public void MessageReceived(LogType type, string tag, string message) {
            Console.WriteLine(string.Format("[{0}] {1}: {2}", type, tag, message));
        }
        private static void CreateConsole() {
            Kernel32.AllocConsole();
        }
    }
}
