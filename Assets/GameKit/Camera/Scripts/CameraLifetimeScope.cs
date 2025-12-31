using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using VContainer;

namespace GameKit.Camera
{
    public sealed class CameraLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<CameraContainer>(Lifetime.Singleton);
            builder.Register<ActiveCameraPoseProvider>(Lifetime.Singleton);
            builder.Register<ActiveCameraInputProxy>(Lifetime.Singleton);
        }
    }
}