using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wafer.UI.Views;

namespace Wafer.UI.Views {
    public static class ViewHelper {
        public static IDictionary<string, Type> GetViewTypes() {
            var views = new Dictionary<string, Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies) {
                var types = assembly.GetTypes()
                    .Where(m => m.IsClass && m.GetInterface("IView") != null);

                foreach (var type in types) {
                    var attributes = type.GetCustomAttributes(typeof (ViewNameAttribute), false);

                    foreach (var attribute in attributes) {
                        views.Add(((ViewNameAttribute)attribute).Name, type);
                    }
                }
            }

            return views;
        }
    }
}
