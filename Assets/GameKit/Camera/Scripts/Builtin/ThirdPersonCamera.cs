using Unity.Cinemachine;
using UnityEngine;

namespace GameKit.Camera.Builtin
{
    public sealed class ThirdPersonCamera : InputAxisControllerBase<InputAxisReader>, ICamera
    {
        public static readonly CameraName Name = new(nameof(ThirdPersonCamera));
        
        void Start()
        {
            Controllers[0].Input = new InputAxisReader();
            Controllers[1].Input = new InputAxisReader();
        }

        void ICamera.SetEnable(bool enable)
        {
            gameObject.SetActive(enable);
        }

        void ICamera.SetLookInput(Vector2 input)
        {
            Controllers[0].Input.SetAxisInput(input.x);
            Controllers[1].Input.SetAxisInput(input.y);
        }

        void Update()
        {
            if (Application.isPlaying)
            {
                UpdateControllers();
            }
        }
    }
}