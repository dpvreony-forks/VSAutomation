using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace VSAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleMethods = Assembly.GetExecutingAssembly()
                .GetTypes()
                .SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .Where(x => Attribute.IsDefined(x, typeof(SampleAttribute)));

            foreach (var sampleMethod in sampleMethods)
            {
                var sampleAttribute = sampleMethod.GetCustomAttributes(typeof(SampleAttribute), false).Single() as SampleAttribute;

                Console.WriteLine("Running sample {0}: {1}.", sampleAttribute.Id, sampleAttribute.Description);
                
                sampleMethod.Invoke(null, null);
            }

            Console.WriteLine();
            if (Debugger.IsAttached)
                Console.ReadLine();
        }
    }
}
