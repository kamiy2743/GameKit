using System;

namespace GameKit.Id
{
    public abstract record BaseGuid
    {
        public Guid Value { get; }

        protected BaseGuid()
        {
            Value = Guid.NewGuid();
        }
        
        protected BaseGuid(Guid value)
        {
            Value = value;
        }
        
        protected BaseGuid(string value)
        {
            Value = Guid.Parse(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}