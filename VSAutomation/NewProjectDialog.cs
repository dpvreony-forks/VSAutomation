using System;
using System.Threading;
using System.Windows.Automation;

namespace VSAutomation
{
    public class NewProjectDialog
    {
        readonly AutomationElement newProjectDialog;
        
        public NewProjectDialog(AutomationElement newProjectDialog)
        {
            this.newProjectDialog = newProjectDialog;

            var condition = new AndCondition(new Condition[] 
            {
                new PropertyCondition(AutomationElement.ClassNameProperty, "TreeView"),
                new PropertyCondition(AutomationElement.AutomationIdProperty, "Installed Templates"),
            });

            var installedTemplates = newProjectDialog.FindFirst(
                    TreeScope.Descendants,
                    condition);

            InstalledTemplates = new TreeView(installedTemplates);

            condition = new AndCondition(new Condition[] 
            {
                new PropertyCondition(AutomationElement.ClassNameProperty, "ListView"),
                new PropertyCondition(AutomationElement.AutomationIdProperty, "lvw_Extensions"),
            });

            var templateList = newProjectDialog.FindFirst(
                    TreeScope.Descendants,
                    condition);

            TemplateList = new ListView(templateList);

            condition = new AndCondition(new Condition[] 
            {
                new PropertyCondition(AutomationElement.ClassNameProperty, "Button"),
                new PropertyCondition(AutomationElement.AutomationIdProperty, "btn_OK"),
            });

            var okButton = newProjectDialog.FindFirst(
                    TreeScope.Descendants,
                    condition);

            OKButton = new Button(okButton);
        }

        public TreeView InstalledTemplates { get; private set; }
        public Button OKButton { get; private set; }
        public ListView TemplateList { get; private set; }
    }
}
