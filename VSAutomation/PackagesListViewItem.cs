using System;
using System.Windows.Automation;

namespace VSAutomation
{
    public class PackagesListViewItem
    {
        readonly SelectionItemPattern _selectionItemPattern;
        readonly AutomationElement _rootAutomationElement;

        public PackagesListViewItem(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;

            object pattern;
            if (rootAutomationElement.TryGetCurrentPattern(SelectionItemPattern.Pattern, out pattern))
                _selectionItemPattern = pattern as SelectionItemPattern;
            else
                throw new InvalidOperationException("The automation element does not support the expected selection item pattern.");
        }

        public Button InstallButton
        {
            get
            {
                var condition = new AndCondition(new Condition[] 
                {
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                    new PropertyCondition(AutomationElement.NameProperty, "Install"),
                });

                var okButton = _rootAutomationElement.FindFirst(
                    TreeScope.Descendants,
                    condition);

                return new Button(okButton);
            }
        }

        public string Text 
        {
            get
            {
                return _rootAutomationElement.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
            }
        }

        public void Select()
        {
            _selectionItemPattern.Select();
        }
    }
}
