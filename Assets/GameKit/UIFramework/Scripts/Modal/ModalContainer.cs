using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalContainer
    {
        readonly UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer;
        readonly ModalStateHolder modalStateHolder;

        public ModalContainer(
            UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer,
            ModalStateHolder modalStateHolder
        )
        {
            this.modalContainer = modalContainer;
            this.modalStateHolder = modalStateHolder;
        }
        
        public async UniTask<ModalId> PushAsync(
            ModalName modalName,
            bool playAnimation = true,
            CancellationToken ct = default
        )
        {
            ct.ThrowIfCancellationRequested();

            var modalId = new ModalId();
            modalStateHolder.Add(modalId, new EmptyModalState());
            try
            {
                await modalContainer.Push(
                    modalName.ResourceKey,
                    playAnimation,
                    modalId.ToString(),
                    onLoad: x => ((BaseModal)x.modal).SetId(modalId)
                );
            }
            catch (Exception)
            {
                modalStateHolder.Remove(modalId);
                throw;
            }
            return modalId;
        }

        public async UniTask PopAsync(int popCount = 1, CancellationToken ct = default) 
        {
            ct.ThrowIfCancellationRequested();

            var targetModalIds = modalContainer.OrderedModalIds
                .TakeLast(popCount)
                .Select(id => new ModalId(id));
            try
            {
                await modalContainer.Pop(true, popCount);
            }
            finally
            {
                foreach (var modalId in targetModalIds)
                {
                    modalStateHolder.Remove(modalId);
                }
            }
        }

        public async UniTask PopAllAsync(CancellationToken ct)
        {
            await PopAsync(popCount: modalContainer.OrderedModalIds.Count, ct: ct);
        }

        public BaseModal? GetActiveModal()
        {
            if (modalContainer.OrderedModalIds.Count == 0)
            {
                return null;
            }

            var id = modalContainer.OrderedModalIds[^1];
            return modalContainer.Modals[id] as BaseModal;
        }
        
        public bool IsTransitioning()
        {
            return modalContainer.IsInTransition;
        }
    }
}