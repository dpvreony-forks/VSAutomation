using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace VSAutomation
{
    public class TreeView
    {
        readonly AutomationElement _rootAutomationElement;

        public TreeView(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;
        }

        public virtual List<TreeViewItem> Items
        {
            get
            {
                var items = _rootAutomationElement.FindAll(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem));

                return items.OfType<AutomationElement>().Select(item => new TreeViewItem(item)).ToList();
            }
        }
    }
}
