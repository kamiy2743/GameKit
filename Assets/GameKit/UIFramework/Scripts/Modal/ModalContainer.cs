using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalContainer
    {
        readonly UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer;
        public ModalContainer(UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer)
        {
            this.modalContainer = modalContainer;
        }
        
        public async UniTask PushAsync(
            ModalName modalName,
            bool playAnimation = true,
            CancellationToken ct = default
        )
        {
            await modalContainer.Push(modalName.ResourceKey, playAnimation);
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