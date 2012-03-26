using System;
using System.Windows.Automation;

namespace VSAutomation
{
    public static class ExtensionMethods
    {
        public static MenuItem MenuItem(
            this VisualStudio visualStudio, 
            string text)
        {
            var menuBar = visualStudio.AutomationElement.FindFirst(
                TreeScope.Children, 
                new PropertyCondition(AutomationElement.AutomationIdProperty, "MenuBar"));
            
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

        public static NewProjectDialog NewProjectDialog(this VisualStudio visualStudio)
        {
            var newProjectDialog = visualStudio.AutomationElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, "New Project"));

            if (newProjectDialog == null)
                return null;
            else
                return new NewProjectDialog(newProjectDialog);
        }
    }
}
