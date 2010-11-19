using System;

namespace VSAutomation
{
    static class Samples
    {
        [Sample(1, "Start Visual Studio, then click File, then Exit, in the menu")]
        public static void OpenAndCloseVs()
        {
            using (var vs = VisualStudio.Start())
            {
                vs.MenuItem("File").SubMenuItem("Exit").Click();
            }
        }
    }
}
