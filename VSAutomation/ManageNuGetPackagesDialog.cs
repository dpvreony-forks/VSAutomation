using System.Windows.Automation;

namespace VSAutomation
{
    public class ManageNuGetPackagesDialog
    {
        private readonly AutomationElement _rootAutomationElement;

        public ManageNuGetPackagesDialog(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;
        }

        public Button CloseButton
        {
            get
            {
                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "Close"),
                });

                var closeButton = _rootAutomationElement.FindFirst(
                    TreeScope.Descendants,
                    condition);

                return new Button(closeButton);
            }
        }
        
        public LicenseAcceptanceDialog LicenseAcceptanceDialog
        {
            get
            {
                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                    new PropertyCondition(AutomationElement.NameProperty, "License Acceptance"),
                });

                var dialog = _rootAutomationElement.FindFirst(
                    TreeScope.Descendants,
                    condition);

                return new LicenseAcceptanceDialog(dialog);
            }
        }
        
        public SelectableTreeView OnlinePanel
        {
            get
            {
                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem),
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "Online"),
                });
                
                var onlinePanel = _rootAutomationElement.FindFirst(
                    TreeScope.Descendants,
                    condition);

                return new SelectableTreeView(onlinePanel);
            }
        }

        public PackagesListView Packages
        {
            get
            {
                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataGrid),
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "lvw_Extensions"),
                });

                var packagesList = _rootAutomationElement.FindFirst(
                    TreeScope.Descendants,
                    condition);

                return new PackagesListView(packagesList);
            }
        }
    }
}
