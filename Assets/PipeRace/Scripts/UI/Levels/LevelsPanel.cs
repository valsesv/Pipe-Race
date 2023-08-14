using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] private Button endlessLevelButton;

    [Inject] private LevelController _levelController;
    
    private void Start()
    {
        endlessLevelButton.onClick.AddListener(_levelController.StartEndlessLevel);
    }
}
