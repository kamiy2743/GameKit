using GameKit.Id;

namespace GameKit.UIFramework.Modal
{
    public sealed record ModalId : BaseGuid
    {
        public ModalId()
        {
        }
        
        public ModalId(string value) : base(value)
        {
        }
    }
}