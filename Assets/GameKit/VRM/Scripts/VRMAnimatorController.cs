using UnityEngine;

namespace GameKit.VRM
{
    public sealed class VRMAnimatorController : MonoBehaviour
    {
        RuntimeAnimatorController animatorController;
        
        public void SetUp(RuntimeAnimatorController animatorController)
        {
            this.animatorController = animatorController;
        }
    }
}