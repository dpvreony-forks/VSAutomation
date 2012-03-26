using System;
using System.Diagnostics;
using System.Windows.Automation;
using System.Threading;

namespace VSAutomation
{
    public class VisualStudio : IDisposable
    {
        readonly Process process;
        readonly AutomationElement visualStudio;

        public VisualStudio(Process process)
        {
            this.process = process;

            visualStudio = AutomationElement.FromHandle(process.MainWindowHandle);
        }

        public AutomationElement AutomationElement 
        {
            get { return visualStudio; }
        }

        public Menu ContextMenu
        {
            get
            {
                Thread.Sleep(1000);

                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ClassNameProperty, "ContextMenu"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Menu),
                });

                var contextMenu = AutomationElement.FindFirst(
                        TreeScope.Descendants,
                        condition);

                return new Menu(contextMenu);
            }
        }

        public ManageNuGetPackagesDialog ManageNuGetPackagesDialog
        {
            get
            {
                Thread.Sleep(1000);

                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                    new PropertyCondition(AutomationElement.NameProperty, Title.Substring(0, Title.IndexOf("-")) + "- Manage NuGet Packages"),
                    
                });

                var dialog = AutomationElement.FindFirst(
                        TreeScope.Descendants,
                        condition);

                return new ManageNuGetPackagesDialog(dialog);
            }
        }
        
        public Menu Menu
        {
            get
            {
                var menuBar = AutomationElement.FindFirst(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "MenuBar"));
                
                return new Menu(menuBar);
            }
        }

        public NewProjectDialog NewProjectDialog
        {
            get
            {
                var newProjectDialog = AutomationElement.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "NewProjectDialog1"));

                if (newProjectDialog == null)
                    return null;
                else
                    return new NewProjectDialog(newProjectDialog);
            }
        }

        public TreeView SolutionExplorer
        {
            get
            {
                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tree),
                    new PropertyCondition(AutomationElement.NameProperty, "Solution Explorer"),
                });

                var solutionExplorer = AutomationElement.FindFirst(
                        TreeScope.Descendants,
                        condition);

                return new TreeView(solutionExplorer);
            }
        }

        public static VisualStudio Start()
        {
            var process = Process.Start(@"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe"); 

            WaitForStart(process.Id);

            return new VisualStudio(Process.GetProcessById(process.Id));
        }

        public string Title
        {
            get
            {
                return AutomationElement.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
            }
        }

        public void WaitForReady()
        {
            Thread.Sleep(5000);
            
            var limit = DateTime.UtcNow.AddSeconds(30);

            while (DateTime.UtcNow < limit)
            {
                try
                {
                    object pattern;
                    if (visualStudio.TryGetCurrentPattern(WindowPattern.Pattern, out pattern))
                    {
                        var window = (WindowPattern)pattern;
                        if (window.Current.WindowInteractionState == WindowInteractionState.ReadyForUserInteraction)
                            return;
                    }
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }

            throw new Exception("Visual Studio failed to start and be ready for automation within the time limit.");
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