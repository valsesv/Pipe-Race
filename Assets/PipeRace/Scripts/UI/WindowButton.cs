using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class WindowButton : MonoBehaviour
{
    [SerializeField] private MenuWindows windowType;
    [Inject] private WindowsManager _windowsManager;
    
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchToWindow);
    }


    private void SwitchToWindow()
    {
        Debug.Log($"Click on window Button {gameObject.name}");
        _windowsManager.SwitchToWindow(windowType);
    }
}
