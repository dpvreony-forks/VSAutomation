using System;

namespace VSAutomation
{
    public class SampleAttribute : Attribute
    {
        public SampleAttribute(int id, string description)
        {
            Id = id;
            Description = description;
        }
        
        public string Description { get; private set; }
        public int Id { get; private set; }
    }
}
