using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image itemImage;
    [SerializeField] private Sprite lockSprite;
    [Space] 
    [SerializeField] private TextMeshProUGUI levelText;

    [Inject] private LevelController _levelController;

    private int LevelIndex => transform.GetSiblingIndex() + 1;
    
    private void Awake()
    {
        InitGraphics();
        
        button.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        _levelController.LoadLevel(LevelIndex);
    }

    private void InitGraphics()
    {
        var levelState = PlayerPrefs.GetInt("LevelPassed" + LevelIndex);
        
        switch (levelState)
        {
            case 0:
                itemImage.sprite = lockSprite;
                levelText.text = "";
                break;
            case 1:
                itemImage.color = Color.yellow;
                levelText.text = $"{LevelIndex}";
                break;
            case 2:
                itemImage.color = Color.white;
                levelText.text = $"{LevelIndex}";
                break;
        }
    }
}
