using UnityEngine;

namespace GameKit.Camera
{
    public interface ICamera
    {
        static CameraName Name;
        void SetEnable(bool enable);
        void SetLookInput(Vector2 input);
    }
}