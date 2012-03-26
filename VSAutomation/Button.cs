using System.Threading;
using System.Windows.Automation;

namespace VSAutomation
{
    public class Button
    {
        private readonly AutomationElement _rootAutomationElement;

        public Button(AutomationElement rootAutomationElement)
        {
            _rootAutomationElement = rootAutomationElement;
        }

        public string Text 
        {
            get
            {
                return _rootAutomationElement.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
            }
        }

        public void Click()
        {
            object pattern;
            
            if (_rootAutomationElement.TryGetCurrentPattern(InvokePattern.Pattern, out pattern))
                ((InvokePattern)pattern).Invoke();

            Thread.Sleep(1000);
        }
    }
}
