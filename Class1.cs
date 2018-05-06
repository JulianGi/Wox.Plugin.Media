using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Wox.Plugin;
using System.Windows.Interop;
using System.Windows.Forms;

namespace Wox.Plugin.Media
{
    public class Plugin : IPlugin
    {
        [DllImport("User32.dll")]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);
        public void Init(PluginInitContext context)
        { 
        }

        public List<Result> Query(Query query)
        {

            var queryString = query.Search.ToLower();
            List<Result> results = new List<Result>();
            var icon = "image.png";
            Dictionary<string, Result> QueryResults = new Dictionary<string, Result>
            {
                {"pa", Result("Play/Pause", "Play/Pause the current track", icon, Action("pause"))},
                {"pl", Result("Play/Pause", "Play/Pause the current track", icon, Action("pause"))},
                {"ne", Result("Next", "Skip the current track", icon, Action("next"))},
                {"pr", Result("Previous", "Go to the last track", icon, Action("previous"))},
                {"mu", Result("Mute/Unmute", "Toggle mute", icon, Action("mute"))},
                {"sk", Result("Skip", "Skip the current track", icon, Action("next"))}

            };

            foreach (var x in QueryResults)
            {
                if (queryString.StartsWith(x.Key))
                    {
                        results.Add(x.Value);
                    }
            }
            return results;
        }

        private static Result Result(String title, String subtitle, String icon, Func<ActionContext, bool> action)
        {
            return new Result()
            {
                
                Title = title,
                SubTitle = subtitle,
                IcoPath = icon,
                Action = action
            };
        }

        // The Action method is called after the user selects the item
        private static Func<ActionContext, bool> Action(String text)
        {
            return e =>
            {
                switch (text)
                {
                    case ("pause"):
                        keybd_event(0xB3, 0, 0x0001 | 0, 0);
                        break;
                    case ("next"):
                        keybd_event(0xB0, 0, 0x0001 | 0, 0);
                        break;
                    case ("previous"):
                        keybd_event(0xB1, 0, 0x0001 | 0, 0);
                        break;
                    case ("mute"):
                        keybd_event(0xAD, 0, 0x0001 | 0, 0);
                        break;
                   
                    default:
                        break;
                }
                // return false to tell Wox don't hide query window, otherwise Wox will hide it automatically
                return false;
            };
        }

    }
       
}


