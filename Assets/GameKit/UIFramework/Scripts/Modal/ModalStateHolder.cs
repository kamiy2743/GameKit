using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using R3;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalStateHolder
    {
        readonly ObservableDictionary<ModalId, IModalState> modalStates = new();
        
        public void Add(ModalId modalId, IModalState modalState)
        {
            modalStates.Add(modalId, modalState);
        }
        
        public void Update(ModalId modalId, IModalState modalState)
        {
            if (!modalStates.ContainsKey(modalId))
            {
                throw new InvalidOperationException($"モーダルが存在しません: {modalId}");
            }
            modalStates[modalId] = modalState;
        }
        
        public void Remove(ModalId modalId)
        {
            modalStates.Remove(modalId);
        }
        
        public T Get<T>(ModalId modalId) where T : IModalState
        {
            if (!modalStates.TryGetValue(modalId, out var state))
            {
                throw new InvalidOperationException($"モーダルが存在しません: {modalId}");
            }
            if (state is not T typedState)
            {
                throw new InvalidOperationException($"モーダル状態の型が異なります: {modalId}, Expected: {typeof(T)}, Actual: {state.GetType()}");
            }
            return typedState;
        }
        
        public async UniTask WaitForStateAsync<T>(ModalId modalId, T targetState, CancellationToken ct)
            where T : IModalState
        {
            if (!modalStates.TryGetValue(modalId, out var state))
            {
                throw new InvalidOperationException($"モーダルが存在しません: {modalId}");
            }
            if (EqualsState(state, targetState))
            {
                return;
            }

            await modalStates.ObserveChanged()
                .Where(e => e.NewItem.Key.Equals(modalId))
                .Where(e => EqualsState(e.NewItem.Value, targetState))
                .FirstAsync(ct);
        }

        static bool EqualsState<T>(IModalState state, T targetState)
            where T : IModalState
        {
            return state is T typedState && typedState.Equals(targetState);
        }
    }
}