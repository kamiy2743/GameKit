using UnityEngine;

namespace GameKit.Camera
{
    public sealed class ActiveCameraInputProxy
    {
        readonly CameraContainer cameraContainer;

        public ActiveCameraInputProxy(CameraContainer cameraContainer)
        {
            this.cameraContainer = cameraContainer;
        }

        public void SetLookInput(Vector2 input)
        {
            cameraContainer.GetActiveCamera()?.SetLookInput(input);
        }
    }
}