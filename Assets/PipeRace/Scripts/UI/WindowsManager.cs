using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class WindowsManager : MonoBehaviour
    {
        [FormerlySerializedAs("menuWindows")] [SerializeField] private WindowType windowType;
        [SerializeField] private CanvasGroup[] windows;

        private const float SwapDuration = 0.25f;

        private void Start()
        {
            SwitchToWindow(WindowType.Main);
        }

        public void SwitchToWindow(WindowType windowType)
        {
            for (var i = 0; i < windows.Length; i++)
            {
                CanvasGroupSwap(windows[i], (int)windowType == i);
            }
        }
        
        public static void CanvasGroupSwap(CanvasGroup canvasGroup, bool isEnabled)
        {
            canvasGroup.DOFade(isEnabled? 1 : 0, SwapDuration);

            canvasGroup.interactable = isEnabled;
            canvasGroup.blocksRaycasts = isEnabled;
        }
    }
}