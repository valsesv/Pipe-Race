using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField] private MenuWindows menuWindows;
        [SerializeField] private CanvasGroup[] windows;

        private const float SwapDuration = 0.25f;

        private void Start()
        {
            SwitchToWindow(MenuWindows.Main);
        }

        public void SwitchToWindow(MenuWindows windowType)
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