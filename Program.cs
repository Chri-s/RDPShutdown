using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using RemoteDesktopShutdown.Properties;

namespace RemoteDesktopShutdown
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            List<ShutdownAction> actions = new List<ShutdownAction>()
            {
                new ShutdownAction(Resources.SignOut, Session.SignOut),
                new ShutdownAction(Resources.Shutdown, Session.Shutdown),
                new ShutdownAction(Resources.Restart, Session.Restart)
            };

            using (MainForm form = new MainForm())
            {
                form.AddActions(actions);

                int selectedIndex = GetCommandLineIndex();
                if (selectedIndex >= actions.Count)
                {
                    selectedIndex = actions.Count - 1;
                }
                else if (selectedIndex < 0)
                {
                    selectedIndex = 0;
                }

                form.SelectedAction = actions[selectedIndex];

                if (form.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    form.SelectedAction.Action();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.MsgBoxTitle);
                }
            }
        }

        static int GetCommandLineIndex()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length != 2)
                return 0;

            int index;

            if (int.TryParse(args[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out index))
            {
                // Commandline argument starts with 1: 1 => SignOut, 2 => Shutdown, 3 => Restart
                index--;
            }
            else
            {
                index = 0;
            }

            return index;
        }
    }
}