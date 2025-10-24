using System.Collections.Generic;

namespace GameKit.Camera
{
    public sealed class CameraContainer
    {
        readonly Dictionary<CameraName, ICamera> cameras = new();

        ICamera? activeCamera;
        
        public void Register(CameraName cameraName, ICamera camera)
        {
            cameras.Add(cameraName, camera);
        }

        public void Deregister(CameraName cameraName)
        {
            cameras.Remove(cameraName);
        }

        public ICamera? GetActiveCamera()
        {
            return activeCamera;
        }

        public void ChangeCamera(CameraName cameraName)
        {
            if (!cameras.ContainsKey(cameraName))
            {
                throw new KeyNotFoundException($"カメラが見つかりません: {cameraName}");
            }

            foreach (var (key, camera) in cameras)
            {
                var isActive = key.Equals(cameraName);
                camera.SetEnable(isActive);
                activeCamera = camera;
            }
        }
    }
}