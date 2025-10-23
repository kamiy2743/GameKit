using Unity.Cinemachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameKit.Camera
{
    public sealed class InputAxisReader : IInputAxisReader
    {
        float input;
        
        public void SetAxisInput(float input)
        {
            this.input = input;
        }
        
        float IInputAxisReader.GetValue(Object context, IInputAxisOwner.AxisDescriptor.Hints hint)
        {
            var scale = 1 / Time.deltaTime;

            if (hint == IInputAxisOwner.AxisDescriptor.Hints.X)
            {
                return input * scale;
            }

            if (hint == IInputAxisOwner.AxisDescriptor.Hints.Y)
            {
                return -input * scale;
            }

            return 0;
        }
    }
}