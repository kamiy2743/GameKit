namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed record ConfirmModalState : IModalState
    {
        public static readonly ConfirmModalState Pending = new(0);
        public static readonly ConfirmModalState Ok = new(1);
        public static readonly ConfirmModalState Cancel = new(2);

        readonly int index;

        public ConfirmModalState(int index)
        {
            this.index = index;
        }
    }
}