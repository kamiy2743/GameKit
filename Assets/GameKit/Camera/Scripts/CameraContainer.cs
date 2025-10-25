using System;
using System.Collections.Generic;

namespace GameKit.Camera
{
    public sealed class CameraContainer
    {
        readonly List<ICamera> cameras = new();

        ICamera? activeCamera;
        
        public void Register<T>(T camera) where T : ICamera
        {
            cameras.Add(camera);
        }

        public void Deregister<T>() where T : ICamera
        {
            cameras.RemoveAll(x => x is T);
        }

        public ICamera? GetActiveCamera()
        {
            return activeCamera;
        }

        public bool IsActiveCamera<T>() where T : ICamera
        {
            return activeCamera is T;
        }

        public void ChangeCamera<T>() where T : ICamera
        {
            if (cameras.Find(x => x is T)  == null)
            {
                throw new InvalidOperationException($"カメラが見つかりません: {typeof(T).FullName}");
            }

            foreach (var camera in cameras)
            {
                var isActive = camera is T;
                camera.SetEnable(isActive);
                if (isActive)
                {
                    activeCamera = camera;
                }
            }
        }
    }
}