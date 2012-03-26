using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Automation;

namespace VSAutomation
{
    public class SelectableTreeView : TreeView
    {
        private readonly AutomationElement _rootAutomationElement;

        public SelectableTreeView(
            AutomationElement rootAutomationElement)
            : base(rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;
        }

        public bool IsSelected
        {
            get
            {
                object pattern;
                if (_rootAutomationElement.TryGetCurrentPattern(SelectionItemPattern.Pattern, out pattern))
                    return ((SelectionItemPattern) pattern).Current.IsSelected;
                else
                    throw new InvalidOperationException("The automation element does not support the expected selection item pattern.");
            }
        }
        
        public override List<TreeViewItem> Items
        {
            get
            {
                if (!IsSelected)
                    return new List<TreeViewItem>();

                var treeView = _rootAutomationElement.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tree));

                var items = treeView.FindAll(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem));

                return items.OfType<AutomationElement>().Select(item => new TreeViewItem(item)).ToList();
            }
        }

        public void Select()
        {
            object pattern;
            if (_rootAutomationElement.TryGetCurrentPattern(SelectionItemPattern.Pattern, out pattern))
                ((SelectionItemPattern)pattern).Select();
            else
                throw new InvalidOperationException("The automation element does not support the expected selection item pattern.");

            Thread.Sleep(5000);
        }
    }
}
