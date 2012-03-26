using System;
using System.Linq;
using System.Windows.Automation;
using System.Collections.Generic;

namespace VSAutomation
{
    public class Menu
    {
        readonly AutomationElement menu;
        
        public Menu(AutomationElement menu)
        {
            this.menu = menu;

            //Items = new MenuItemCollection(menu);
        }

        //public MenuItemCollection Items { get; private set; }
        
        public IList<MenuItem> Items
        {
            get
            {
                var items = menu.FindAll(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem));

                return items.OfType<AutomationElement>().Select(item => new MenuItem(item)).ToList();
            }
        }
    }
}
