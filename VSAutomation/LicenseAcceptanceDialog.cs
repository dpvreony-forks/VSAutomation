using System.Windows.Automation;

namespace VSAutomation
{
    public class LicenseAcceptanceDialog
    {
        private readonly AutomationElement _rootAutomationElement;

        public LicenseAcceptanceDialog(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;
        }

        public Button AcceptButton
        {
            get
            {
                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                    new PropertyCondition(AutomationElement.NameProperty, "I Accept"),
                });

                var acceptButton = _rootAutomationElement.FindFirst(
                    TreeScope.Descendants,
                    condition);

                return new Button(acceptButton);
            }
        }
    }
}
