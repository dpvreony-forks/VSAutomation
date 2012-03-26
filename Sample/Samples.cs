using System;
using System.Linq;
using System.Threading;

namespace VSAutomation
{
    static class Samples
    {
        //[Sample(1, "Start Visual Studio, then click File, then Exit, in the menu")]
        //public static void OpenAndCloseVs()
        //{
        //    using (var vs = VisualStudio.Start())
        //    {
        //        vs.Menu.Items["File"].Items["Exit"].Click();
        //    }
        //}

        [Sample(2, "Create an class library and install EF via NuGet")]
        public static void CreateMvc3Project()
        {
            using (var vs = VisualStudio.Start())
            {
                var fileMenu = vs.Menu.Items.Single(item => item.Text == "File");
                fileMenu.Click();
                var newProjectMenuItem = fileMenu.Items.Single(item => item.Text == "New Project...");
                newProjectMenuItem.Click();

                var npd = vs.NewProjectDialog;
                npd.InstalledTemplates
                    .Items.First(item => item.Text == "Visual C#")
                    .Items.First(item => item.Text == "Windows")
                    .Select();
                npd.TemplateList
                    .Items.First(item => item.Text == "Class Library")
                    .Select();
                npd.OKButton.Click();

                vs.WaitForReady();

                Mouse.MoveTo(vs.SolutionExplorer.Items.First().ClickablePoint);
                Mouse.Click(MouseButton.Right);

                vs.ContextMenu.Items.Single(item => item.Text == "Manage NuGet Packages...").Click();

                var nugetDialog = vs.ManageNuGetPackagesDialog;
                nugetDialog.OnlinePanel.Select();
                var efPackage = nugetDialog.Packages.Items.Single(item => item.Text == "EntityFramework");
                efPackage.Select();
                efPackage.InstallButton.Click();
                Thread.Sleep(2500);
                nugetDialog.LicenseAcceptanceDialog.AcceptButton.Click();
                Thread.Sleep(5000);
                nugetDialog.CloseButton.Click();

                Thread.Sleep(30000);
            }
        }

        //[Sample(3, "Install a NuGet package")]
        //public static void InstallNugetPackage()
        //{
        //    using (var vs = VisualStudio.Start())
        //    {
                
        //    }
        //}
    }
}
