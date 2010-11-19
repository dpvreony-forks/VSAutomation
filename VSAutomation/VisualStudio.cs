using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Automation;
using System.Threading;

namespace VSAutomation
{
    public class VisualStudio : IDisposable
    {
        readonly AutomationElement menuBar;
        readonly Process process;
        readonly AutomationElement visualStudio;

        public VisualStudio(Process process)
        {
            this.process = process;

            visualStudio = AutomationElement.FromHandle(process.MainWindowHandle);
            menuBar = visualStudio.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.AutomationIdProperty, "MenuBar"));
        }

        public MenuItem MenuItem(string text)
        {
            var condition = new AndCondition(new Condition[] {
                new PropertyCondition(AutomationElement.ClassNameProperty, "MenuItem"),
                new PropertyCondition(AutomationElement.NameProperty, text),
            });

            var menuItem = menuBar.FindFirst(TreeScope.Children, condition);

            if (menuItem == null)
                return null;
            else
                return new MenuItem(menuItem);
        }

        public static VisualStudio Start()
        {
            var process = Process.Start("devenv.exe"); 

            WaitForStart(process.Id);

            return new VisualStudio(Process.GetProcessById(process.Id));
        }

        static void WaitForStart(int processId)
        {
            var limit = DateTime.Now.AddSeconds(30);

            while (DateTime.Now < limit)
            {
                try
                {
                    var process = Process.GetProcessById(processId);
                    var ae = AutomationElement.FromHandle(process.MainWindowHandle);
                    object pattern;
                    if (ae.TryGetCurrentPattern(WindowPattern.Pattern, out pattern))
                    {
                        var window = (WindowPattern)pattern;
                        if (window.Current.WindowInteractionState == WindowInteractionState.ReadyForUserInteraction)
                            return;
                    }

                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }

            throw new Exception("Visual Studio failed to start and be ready for automation within the time limit.");
        }

        public void Dispose()
        {
            try
            {
                process.Kill();
            }
            catch { }
        }
    }
}
