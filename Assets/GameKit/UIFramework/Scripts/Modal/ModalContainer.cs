using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.UIFramework.UnityScreenNavigatorResource;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalContainer
    {
        readonly UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer;
        public ModalContainer(UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer)
        {
            this.modalContainer = modalContainer;
        }
        
        public async UniTask PushAsync<T>(bool playAnimation = true, CancellationToken ct = default) where T : BaseModal 
        {
            var resourceKey = ResourceKey.FromGenerics<T>();
            await modalContainer.Push(resourceKey.Value, playAnimation);
        }

        public async UniTask PopAsync(int popCount = 1, CancellationToken ct = default) 
        {
            await modalContainer.Pop(true, popCount);
        }

        public async UniTask PopAllAsync(CancellationToken ct)
        {
            await PopAsync(popCount: modalContainer.OrderedModalIds.Count, ct: ct);
        }
    }
}