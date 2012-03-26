using System.Collections.Generic;
using System.Windows.Automation;

namespace VSAutomation
{
    public class ListView
    {
        readonly AutomationElement _rootAutomationElement;

        public ListView(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;

            Items = new List<ListViewItem>();

            var items = _rootAutomationElement.FindAll(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "ListViewItem"));

            foreach(var item in items)
                Items.Add(new ListViewItem((AutomationElement)item));
        }

        public List<ListViewItem> Items { get; private set; }
    }
}
