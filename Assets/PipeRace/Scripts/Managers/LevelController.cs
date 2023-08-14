using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;

    [SerializeField]
    private Transform levelParent;

    [Space(10)] 
    [SerializeField] private GameObject gameController;
    
    [Inject] private WindowsManager _windowsManager;
    
    public void LoadLevel(int levelIndex)
    {
        Instantiate(levels[levelIndex], levelParent);
        
        StartLevel();
    }
    
    public void StartEndlessLevel()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        _windowsManager.SwitchToWindow(WindowType.Game);
        gameController.SetActive(true);
    }
}
