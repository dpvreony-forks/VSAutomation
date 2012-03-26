using System;
using System.Windows.Automation;

namespace VSAutomation
{
    public class MenuItemCollection
    {
        readonly AutomationElement menu;
        
        public MenuItemCollection(AutomationElement menu)
        {
            this.menu = menu;
        }

        public MenuItem this[string text]
        {
            get
            {
                object pattern;

                if (menu.Current.ClassName == "Menu")
                {
                    var condition = new AndCondition(new Condition[] 
                    {
                        new PropertyCondition(AutomationElement.ClassNameProperty, "MenuItem"),
                        new PropertyCondition(AutomationElement.NameProperty, text),
                    });

                    var menuItem = menu.FindFirst(TreeScope.Children, condition);

                    if (menuItem == null)
                        return null;
                    else
                        return new MenuItem(menuItem);
                }
                else if (menu.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out pattern))
                {
                    var expandCollapsePattern = pattern as ExpandCollapsePattern;

                    if (expandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.Collapsed)
                        expandCollapsePattern.Expand();

                    var condition = new AndCondition(new Condition[] 
                    {
                        new PropertyCondition(AutomationElement.ClassNameProperty, "MenuItem"),
                        new PropertyCondition(AutomationElement.NameProperty, text),
                    });

                    var menuItem = menu.FindFirst(TreeScope.Children, condition);

                    if (menuItem == null)
                        return null;
                    else
                        return new MenuItem(menuItem);
                }
                else
                    return null;
            }
        }
    }
}
