using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject PauseMenu, PauseButton, LevelNext, MoneyCoin;
    public Text ContinueText, GameMoney;
    public Transform Obstacles;
    public static int ReloadLevel;
    public AudioSource GamePlayMusic;
    public AudioClip Level0, GameMusic;
    private void Awake()
    {
        Time.timeScale = 1;
        hazardMove.speed = -0.5f;
        moveScript.speed = 2f;
    }

    private void Start()
    {
        if (ReloadLevel != 0)
        {
            for (int i = 1; i < 3; i++)
            {
                float RoAngle = 22.5f * Random.Range(0, 17);
                Vector3 spawnPosition = new Vector3(0, 0, 57 * i);
                Instantiate(MoneyCoin, spawnPosition, Quaternion.Euler(11 + RoAngle, 90, 0), Obstacles);
            }
            StartCoroutine(Spawn());
        }
        if (ReloadLevel == 0)
        {
            /*GamePlayMusic.clip = Level0;
            GamePlayMusic.Play();*/
        }

        GamePlayMusic.clip = Level0;
        GamePlayMusic.Play();
    }


    private IEnumerator Spawn()
    {
        while (true)
        {
            float RoAngle = 22.5f * Random.Range(0, 17);

            for (int i = 3; i < 15; i++)
            {
                Vector3 spawnPosition = new Vector3(0, 0, 57 * i);
                Instantiate(MoneyCoin, spawnPosition, Quaternion.Euler(11 + RoAngle, 90, 0), Obstacles);
                RoAngle = 22.5f * Random.Range(0, 17);
            }

            GameObject lastcpin = Instantiate(MoneyCoin, new Vector3(0, 0, 57 * 15), Quaternion.Euler(11 + RoAngle, 90, 0), Obstacles);
            while (lastcpin.transform.position.z > 114)
                yield return new WaitForSeconds(0.1f);
            Destroy(lastcpin);
        }
    }
    private void Update()
    {
        GameMoney.text = "" + PlayerPrefs.GetInt("WITgameO");
    }

    public void PauseGame()
    {
            if (Time.timeScale == 1)
            {
                GamePlayMusic.Pause();
                moveScript.c = 1;
                Time.timeScale = 0.00001f;
                PauseButton.SetActive(false);
                PauseMenu.SetActive(true);
            }
            else if (PauseMenu.activeSelf == true || moveScript.c == 1 || moveScript.c == 2)
                StartCoroutine(ContinueGame());
    }

    public void PauseWithoutTimer()
    {
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
        GamePlayMusic.Play();
    }
    
    public IEnumerator ContinueGame()
    {
        PauseMenu.SetActive(false);

        for (int i = 3; i > 0; i--)
        {
            ContinueText.text = "" + i;
            yield return new WaitForSeconds(0.00001f);
        }
        ContinueText.text = "";
        PauseButton.SetActive(true);
        Time.timeScale = 1;
        GamePlayMusic.Play();
    }
    
    public void ReplayGame()
    {
        PlayerPrefs.SetInt("Newlevel", PlayerPrefs.GetInt("Newlevel") - 1);
        PlayerPrefs.SetInt("replaygame", ReloadLevel);
        SceneManager.LoadScene("game");
    }

    public void NextLevel()
    {
        if (ReloadLevel == 0 || ReloadLevel == 24)
        {
            GoHome();
        }
        else
        {
            PlayerPrefs.SetInt("Newlevel", 2);
            ReloadLevel++;
            ReplayGame();
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("game");
    }
}
