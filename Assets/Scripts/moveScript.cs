using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class moveScript : MonoBehaviour
{
    public static float speed;
    public GameObject LooseMenu, Gameogj, PauseButton, ContButton, hazards, coins50;
    private Vector3 PointAround;
    public static int c, countPress = 0, SwapMotion;
    public gameController Game;
    public AudioClip getmoney;
    public List<AudioClip> LevelsIntroduction;
    public AudioSource GamePlayMusic;
    public Text Money;
    private float g = 0, gFactor = 0.1f;

    private void Start()
    {
        SwapMotion = 1;
        transform.RotateAround(PointAround, Vector3.forward, Random.Range(0, 360));

        if (Advertisement.isSupported)
            Advertisement.Initialize("3978561", false);
        c = 0;
        PointAround = Vector3.zero;

        if (Advertisement.IsReady("GetCoins"))
            coins50.SetActive(true);

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerWhenReady());

        StartCoroutine(Stop());
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.05f); //fix begining rotation
        spdr = gFactor = 0.1f;
        spdl = -0.1f;
    }

    private void FixedUpdate()
    {
        if (Application.isEditor)
            ButtonForPC();

        if (gFactor == -1)
            SpeedUp(-1);
        else if (gFactor == -0.1f)
            SpeedDown(-1);

        if (gFactor == 1)
            SpeedUp(1);
        else if (gFactor == 0.1f)
            SpeedDown(1);

        transform.RotateAround(PointAround, Vector3.forward, SwapMotion * g * speed);

        if (Gameogj.activeSelf == false)
            transform.RotateAround(PointAround, -Vector3.forward, 0.06f);
        if (countPress >= 5 && !PlayerPrefs.HasKey("IfAdsOn")) //Check if ads on
        {
            countPress = 0;
            Advertisement.Show("video");
        }
    }
    void ButtonForPC()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (gFactor == -1 || gFactor == 1)
                gFactor /= 10;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            gFactor = -1;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            gFactor = 1;
        else if (gFactor == -1 || gFactor == 1)
            gFactor /= 10;
            
    }
    private void OnCollisionEnter(Collision other)
    {
        if (PlayerPrefs.GetInt("Vibrato") == 1)
            Handheld.Vibrate();
        Time.timeScale = 0.00001f;
        spdl = -0.1f; spdr = 0.1f;  g = 0;
        Destroy(other.gameObject);
        PauseButton.SetActive(false);

        if (gameController.ReloadLevel == 0)
            Level0Script.IsLost = true;
        else
        {
            LooseMenu.SetActive(true);
            GamePlayMusic.Pause();
        }
        
        if (!Advertisement.IsReady("rewardedVideo"))
            ContButton.SetActive(false);
        
        if (ContButton.activeSelf == true)
            StartCoroutine(ContinueGameButton());
    }

    public IEnumerator ContinueGameButton()
    {
        yield return new WaitForSeconds(Time.timeScale * 5);
        ContButton.SetActive(false);
    }

    public void ContinueGame()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
            c = 2;
            countPress = 0;
            hazards.transform.position += new Vector3(0, 0, 10);
            LooseMenu.SetActive(false);
            Game.PauseGame();
        }
    }
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady("Banner"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show("Banner");
    }

    public void RevardVid()
    {
        Advertisement.Show("GetCoins");

        PlayerPrefs.SetInt("WITgameO", PlayerPrefs.GetInt("WITgameO") + 25);
        GetComponent<AudioSource>().PlayOneShot(getmoney);
        coins50.SetActive(false);
        Money.text = "" + PlayerPrefs.GetInt("WITgameO");
        countPress = 0;
    }
    private float spdl, spdr;
    public void changeG(float f)
    {
        if (f > 0)
            spdr = f;
        else
            spdl = f;
        if ((f == -1 || f == 1) || (spdl == -0.1f && spdr == 0.1f))
            gFactor = f;
        else
        {
            if (spdl == -1)
                gFactor = -1;
            if (spdr == 1)
                gFactor = 1;
        }
    }
    public void SpeedUp(int f)
    {
        if (g < 1 && f == 1)
            g += 0.05f;
        if (g > -1 && f == -1)
            g -= 0.05f;
    }

    public void SpeedDown(int f)
    {
        if (f == -1 && g < 0)
            g += 0.05f;
        else if (f == -1)
            g = 0;

        if (f == 1 && g > 0)
            g -= 0.05f;
        else if (f == 1)
            g = 0;
    }

    public void LevelMusicIntroduction(int num)
    {
        GetComponent<AudioSource>().PlayOneShot(LevelsIntroduction[num]);
        // 1 -- level not passed
        // 2 -- level passed
        // 3 -- endless level
    }
}