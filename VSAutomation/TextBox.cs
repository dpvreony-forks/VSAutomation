using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace VSAutomation
{
    public class TextBox
    {
        private readonly AutomationElement _rootAutomationElement;
        readonly ValuePattern valuePattern;

        public TextBox(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;

            object pattern;
            if (_rootAutomationElement.TryGetCurrentPattern(ValuePattern.Pattern, out pattern))
                valuePattern = pattern as ValuePattern;
            else
                throw new InvalidOperationException("The automation element did not have the exepcted ValuePattern.");
        }

        public string Text
        {
            get { return valuePattern.Current.Value; }
            set { valuePattern.SetValue(value); }
        }
    }
}
