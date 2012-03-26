using System;
using System.Threading;
using System.Windows.Automation;
using System.Linq;
using System.Collections.Generic;

namespace VSAutomation
{
    public class MenuItem
    {
        readonly AutomationElement menuItem;
        
        public MenuItem(AutomationElement menuItem)
        {
            this.menuItem = menuItem;

            //Items = new MenuItemCollection(menuItem);
        }

        //public MenuItemCollection Items { get; private set; }

        public IList<MenuItem> Items
        {
            get
            {
                var items = menuItem.FindAll(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem));

                return items.OfType<AutomationElement>().Select(item => new MenuItem(item)).ToList();
            }
        }

        public string Text 
        {
            get
            {
                return menuItem.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
            }
        }

        public void Click()
        {
            object pattern;
            
            if (menuItem.TryGetCurrentPattern(InvokePattern.Pattern, out pattern))
                ((InvokePattern)pattern).Invoke();

            if (menuItem.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out pattern))
            {
                var expandCollapsePattern = pattern as ExpandCollapsePattern;

                if (expandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.Collapsed)
                    expandCollapsePattern.Expand();
                else
                    expandCollapsePattern.Collapse();
            }

            Thread.Sleep(1000);
        }
    }
}
