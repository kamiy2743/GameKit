using UnityEngine;

namespace GameKit.Application
{
    public sealed class Cursor
    {
        public static void SetVisible(bool visible)
        {
            UnityEngine.Cursor.visible = visible;
            UnityEngine.Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}