using System;
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
        }

        public MenuItem SubMenuItem(string text)
        {
            object pattern;

            if (menuItem.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out pattern))
            {
                var expandCollapsePattern = pattern as ExpandCollapsePattern;

                if (expandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.Collapsed)
                    expandCollapsePattern.Expand();

                var aeSubMenuItem = menuItem.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, text));

                if (aeSubMenuItem == null)
                    return null;
                else
                    return new MenuItem(aeSubMenuItem);
            }
            else
                throw new Exception("The menu item has no submenu");
        }
    }
}
