namespace GameKit.UIFramework.Modal
{
    public interface IModalLifetimeScope
    {
        void Run(IPushModalParams? pushModalParams = null);
    }
}