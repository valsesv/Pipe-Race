using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuConrtoller : MonoBehaviour
{
    public List<Text> Texts = new List<Text>();
    public List<GameObject> Levels = new List<GameObject>(), ButtonLevels = new List<GameObject>();
    public GameObject SoundOnButton, SoundOffButton, SettingsPanel,
        ShopPannel, panel, levelsPanel, WinPanel;
    public GameObject gameObj, SorryPanel, Menu, EndZone, PauseButton,
        LeftMove, RightMove, VibrOn, VibrOff;
    public GameObject shopTut;

    public ShopScript shop;
    public Slider SoundSlider;
    private Animator SetAnim, SizeAnimPanel, SizeAnimPanelShop, SizeAnimLevel;
    public moveScript Player;
    public Sprite Locked;
    private bool IfLevels;

    private void Awake()
    {
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

        shop.skin(PlayerPrefs.GetInt("CNNumGameO"));

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
        
        SetAnim = SettingsPanel.GetComponent<Animator>();
        SizeAnimPanel = panel.GetComponent<Animator>();
        SizeAnimPanelShop = ShopPannel.GetComponent<Animator>();
        SizeAnimLevel = levelsPanel.GetComponent<Animator>();

        Texts[0].text = PlayerPrefs.GetFloat("gameOasdfg").ToString("0");
        Texts[1].text = "" + PlayerPrefs.GetInt("WITgameO");
        SoundReg(PlayerPrefs.GetFloat("soundgameO"));

       // QFSW.SIIE.SelectableInversion.clearColor = Color.black;
        for (int i = 1; i < 25; i++)
        {
            if (PlayerPrefs.GetInt("LevelPassed" + i) == 0)
            {
                ButtonLevels[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = "";
                ButtonLevels[i].GetComponent<Image>().sprite = Locked;
            }
            if (PlayerPrefs.GetInt("LevelPassed" + i) == 1)
                ButtonLevels[i].GetComponent<Image>().color = Color.yellow;
            if (PlayerPrefs.GetInt("LevelPassed" + i) == 2)
                ButtonLevels[i].GetComponent<Image>().color = Color.white;
                
        }

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

    public void SettingsOn()
    {
        SettingsPanel.SetActive(true);
        SetAnim.SetInteger("isPlay", 1);
    }
    public void Settingsoff()
    {
        SetAnim.SetInteger("isPlay", 2);
        StartCoroutine(settingsoff());
    }

    IEnumerator settingsoff()
    {
        yield return new WaitForSeconds(0.6f);

        SettingsPanel.SetActive(false);
    }

    public void ShopOn()
    {
        PlayerPrefs.SetInt("shopTutorial", 1);
        shopTut.SetActive(false);
        //ShopPannel.SetActive(true);
        if (IfLevels)
            SizeAnimLevel.SetInteger("isPlay", 2);
        else    
            SizeAnimPanel.SetInteger("isPlay", 2);
        SizeAnimPanelShop.SetInteger("isPlay", 1);
        StartCoroutine(WaitForShop());
    }
    IEnumerator WaitForShop()
    {
        yield return new WaitForSeconds(0.6f);
        shop.SavePos();
    }
    public void Shopff()
    {
        //panel.SetActive(true);
        if (IfLevels)
            SizeAnimLevel.SetInteger("isPlay", 1);
        else    
            SizeAnimPanel.SetInteger("isPlay", 1);
        SizeAnimPanelShop.SetInteger("isPlay", 2);
        //StartCoroutine(ShopOff());
    }

    IEnumerator ShopOff() {
        yield return new WaitForSeconds(0.6f);
        ShopPannel.SetActive(false);   
    }
    public void LoadLevel(int LevelNum)
    {   
        if (PlayerPrefs.GetInt("LevelPassed" + LevelNum) != 0)
        {
            
            if (gameController.ReloadLevel != LevelNum)
            {
                StartCoroutine(Darkskr());
            }
            else
            {
                gameObj.SetActive(true);
                Menu.SetActive(false);
            }
                gameController.ReloadLevel = LevelNum;
            if (LevelNum != 31)
                EndZone.SetActive(true);
            else
            {
                EndZone.SetActive(false);
                if (PlayerPrefs.GetInt("Newlevel") == 1)
                    Player.LevelMusicIntroduction(2);
            }
            Levels[LevelNum].SetActive(true);
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

    IEnumerator Darkskr()
    {
       SizeAnimLevel.SetInteger("isPlay", 5);
        yield return new WaitForSeconds(0.3f);
        gameObj.SetActive(true);
        Menu.SetActive(false);
    }

    public void LevelsOn()
    {
        IfLevels = true;
        SizeAnimPanel.SetInteger("isPlay", 2);
        SizeAnimLevel.SetInteger("isPlay", 1);
        //StartCoroutine(WaitForLevels());
    }

    IEnumerator WaitForLevels()
    {
        yield return new WaitForSeconds(0.5f);
        SizeAnimPanel.SetInteger("isPlay", 4);
        SizeAnimLevel.SetInteger("isPlay", 3);
        yield return new WaitForSeconds(1f);
    }
    public void LevelsOff()
    {
        IfLevels = false;
        SizeAnimPanel.SetInteger("isPlay", 1);
        SizeAnimLevel.SetInteger("isPlay", 2);
        //StartCoroutine(Levelsff());
    }


    IEnumerator Levelsff()
    {
        yield return new WaitForSeconds(0.3f);
        SizeAnimPanel.SetInteger("isPlay", 3);
        SizeAnimLevel.SetInteger("isPlay", 4);
        yield return new WaitForSeconds(1f);
    }

}
