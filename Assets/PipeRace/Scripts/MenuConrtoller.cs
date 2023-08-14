using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuConrtoller : MonoBehaviour
{
    public List<Text> Texts = new List<Text>();
    public GameObject SoundOnButton, SoundOffButton, SettingsPanel,
        ShopPannel, panel, levelsPanel, WinPanel;
    public GameObject gameObj, SorryPanel, Menu, PauseButton,
        LeftMove, RightMove, VibrOn, VibrOff;
    public GameObject shopTut;

    public ShopScript shop;
    public Slider SoundSlider;
    public moveScript Player;
    public Sprite Locked;
    private bool IfLevels;

    private void Awake()
    {
        return;
        // if (!PlayerPrefs.HasKey("HowToDO"))
        //     PlayerPrefs.SetInt("HowToDO", 1); // how to move
        if (!PlayerPrefs.HasKey("soundgameO"))
            PlayerPrefs.SetFloat("soundgameO", 1);
        if (!PlayerPrefs.HasKey("Vibrato"))
            PlayerPrefs.SetInt("Vibrato", 1);
        if (!PlayerPrefs.HasKey("gameOasdfg"))//hscr
            PlayerPrefs.SetFloat("gameOasdfg", 0);
        if (!PlayerPrefs.HasKey("WITgameO"))//mn
            PlayerPrefs.SetInt("WITgameO", 0);
        if (!PlayerPrefs.HasKey("HaveSkin" + 0))//skin
            PlayerPrefs.SetInt("HaveSkin" + 0, 1);
        if (!PlayerPrefs.HasKey("CNNumGameO")) // num of skin
            PlayerPrefs.SetInt("CNNumGameO", 0);

        if (!PlayerPrefs.HasKey("LevelPassed" + 0))
        {
            for (int i = 1; i < 32; i++)
                PlayerPrefs.SetInt("LevelPassed" + i, 0);
            PlayerPrefs.SetInt("LevelPassed" + 0, 1);
            
        }
        if (PlayerPrefs.GetInt("LevelPassed" + 1) == 0)
            LoadLevel(0);
        else if (PlayerPrefs.GetInt("shopTutorial") == 0)
        {
            shopTut.SetActive(true);
        }

        Texts[0].text = PlayerPrefs.GetFloat("gameOasdfg").ToString("0");
        Texts[1].text = "" + PlayerPrefs.GetInt("WITgameO");
        SoundReg(PlayerPrefs.GetFloat("soundgameO"));

        if (PlayerPrefs.HasKey("replaygame"))
        {
            int ok = PlayerPrefs.GetInt("replaygame");
            PlayerPrefs.DeleteKey("replaygame");
            LoadLevel(ok);
        }
        else
        {
            gameController.ReloadLevel = 0;
        }
        PlayerPrefs.SetInt("Newlevel", 1);
    }

    public void SoundReg(float volume)
    {
        PlayerPrefs.SetFloat("soundgameO", volume);
        SoundSlider.value = PlayerPrefs.GetFloat("soundgameO");
        AudioListener.volume = volume;

        if (PlayerPrefs.GetFloat("soundgameO") == 0)
        {
            SoundOnButton.SetActive(false);
            SoundOffButton.SetActive(true);
        }
        else
        {
            SoundOnButton.SetActive(true);
            SoundOffButton.SetActive(false);
        }
    }

    public void VibroReg(int a)
    {
        PlayerPrefs.SetInt("Vibrato", a);

        if (PlayerPrefs.GetInt("Vibrato") == 0)
        {
            VibrOn.SetActive(false);
            VibrOff.SetActive(true);
        }
        else
        {
            Handheld.Vibrate();
            VibrOn.SetActive(true);
            VibrOff.SetActive(false);
        }

    }

    public void LoadLevel(int LevelNum)
    {   
        if (PlayerPrefs.GetInt("LevelPassed" + LevelNum) != 0)
        {
            gameController.ReloadLevel = LevelNum;
            
            if (LevelNum == 31)
            {
                if (PlayerPrefs.GetInt("Newlevel") == 1)
                    Player.LevelMusicIntroduction(2);
            }
            
            if (PlayerPrefs.GetInt("Newlevel") == 1)
                Player.LevelMusicIntroduction(PlayerPrefs.GetInt("LevelPassed" + LevelNum) - 1);
            moveScript.countPress++;
        }
        else if (LevelNum == 31)
        {
            Texts[2].text = "Available after 5";
            SorryPanel.SetActive(true);
        }
    }
}
