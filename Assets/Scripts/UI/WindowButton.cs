using UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WindowButton : MonoBehaviour
{
    [SerializeField] private MenuWindows windowType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchToWindow);
    }


    private void SwitchToWindow()
    {
        
    }
}
