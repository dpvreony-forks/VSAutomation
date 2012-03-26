using System;
using System.Windows.Automation;

namespace VSAutomation
{
    public class ListViewItem
    {
        readonly SelectionItemPattern _selectionItemPattern;
        readonly AutomationElement _rootAutomationElement;

        public ListViewItem(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;

            object pattern;
            if (rootAutomationElement.TryGetCurrentPattern(SelectionItemPattern.Pattern, out pattern))
                _selectionItemPattern = pattern as SelectionItemPattern;
            else
                throw new InvalidOperationException("The automation element does not support the expected selection item pattern.");
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
