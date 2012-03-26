using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace VSAutomation
{
    public class PackagesListView
    {
        readonly AutomationElement _rootAutomationElement;

        public PackagesListView(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;
        }

        public List<PackagesListViewItem> Items
        {
            get
            {
                var items = _rootAutomationElement.FindAll(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem));

                return items.OfType<AutomationElement>().Select(item => new PackagesListViewItem(item)).ToList();
            }
        }
    }
}
