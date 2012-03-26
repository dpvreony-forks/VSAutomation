using System;
using System.Drawing;
using System.Windows.Automation;
using System.Linq;
using System.Collections.Generic;

namespace VSAutomation
{
    public class TreeViewItem
    {
        readonly ExpandCollapsePattern expandCollapsePattern;
        readonly SelectionItemPattern selectionItemPattern;
        readonly AutomationElement templateTreeItem;

        public TreeViewItem(AutomationElement templateTreeItem)
        {
            this.templateTreeItem = templateTreeItem;

            object pattern;

            if (templateTreeItem.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out pattern))
                this.expandCollapsePattern = pattern as ExpandCollapsePattern;

            if (templateTreeItem.TryGetCurrentPattern(SelectionItemPattern.Pattern, out pattern))
                this.selectionItemPattern = pattern as SelectionItemPattern;
            else
                throw new ArgumentException("The automation element did not support the exepcted selection item pattern.", "tree");

            Items = new List<TreeViewItem>();

            var items = templateTreeItem.FindAll(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem));

            foreach (var item in items)
                Items.Add(new TreeViewItem((AutomationElement)item));
        }

        public Point ClickablePoint
        {
            get
            {
                var bounds = templateTreeItem.Current.BoundingRectangle;
                var centerX = (int)(bounds.X + bounds.Width / 2);
                int centerY = (int)(bounds.Y + bounds.Height / 2);
                return new Point(centerX, centerY);
            }
        }
        
        public bool IsExpanded
        {
            get 
            {
                if (IsLeaf)
                    throw new InvalidOperationException("This template tree item is a leaf item and cannot be expanded or collapsed.");
                else
                    return this.expandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.Expanded;
            }
        }
        
        public bool IsLeaf
        {
            get { return expandCollapsePattern == null; }
        }

        public IList<TreeViewItem> Items { get; private set; }

        public string Text 
        {
            get
            {
                return templateTreeItem.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
            }
        }

        public void Collapse()
        {
            if (IsLeaf)
                throw new InvalidOperationException("This template tree item is a leaf item and cannot be collapsed.");
            else
                this.expandCollapsePattern.Collapse();
        }

        public void Expand()
        {
            if (IsLeaf)
                throw new InvalidOperationException("This template tree item is a leaf item and cannot be expanded.");
            else
                this.expandCollapsePattern.Expand();
        }

        public void Select()
        {
            this.selectionItemPattern.Select();
        }
    }
}
