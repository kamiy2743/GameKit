namespace GameKit.Input
{
    public sealed class InputMode
    {
        public static readonly InputMode Default = new(nameof(Default), true);
        
        readonly string value;
        public bool isVisibleCursor { get; }
        
        public InputMode(string value, bool isVisibleCursor)
        {
            this.value = value;
            this.isVisibleCursor = isVisibleCursor;
        }
        
        public bool Contains(InputMode mode)
        {
            return Equals(Default) || Equals(mode);
        }
        
        bool Equals(InputMode other)
        {
            return value == other.value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}