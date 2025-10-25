using UnityEngine;

namespace GameKit.Camera
{
    public interface ICamera
    {
        void SetEnable(bool enable);
        void SetLookInput(Vector2 input);
    }
}