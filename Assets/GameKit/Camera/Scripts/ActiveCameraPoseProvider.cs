using UnityEngine;

namespace GameKit.Camera
{
    public sealed class ActiveCameraPoseProvider
    {
        readonly CameraContainer cameraContainer;

        public ActiveCameraPoseProvider(CameraContainer cameraContainer)
        {
            this.cameraContainer = cameraContainer;
        }

        public Vector3 GetPosition()
        {
            return cameraContainer.GetActiveCamera()!.GetPosition();
        }
    }
}