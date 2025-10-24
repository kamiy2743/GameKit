using Unity.Cinemachine;
using UnityEngine;

namespace GameKit.Camera
{
    public sealed class ThirdPersonCamera : InputAxisControllerBase<InputAxisReader>
    {
        void Start()
        {
            Controllers[0].Input = new InputAxisReader();
            Controllers[1].Input = new InputAxisReader();
        }

        public void SetLookInput(Vector2 input)
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