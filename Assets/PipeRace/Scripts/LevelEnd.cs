using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    public GameObject WinPanel, PauseButton;
    public Text CongText;
    public AudioClip getmoney;
    public AudioSource GamePlayMusic;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GamePlayMusic.Pause();
            PauseButton.SetActive(false);
            WinPanel.SetActive(true);
            Time.timeScale = 0.00001f;

            if (PlayerPrefs.GetInt("LevelPassed" + gameController.ReloadLevel) == 1)
            {
                int getMoneyAmount = 15;
                if (gameController.ReloadLevel > 10)
                    getMoneyAmount = 25;
                if (gameController.ReloadLevel > 20)
                    getMoneyAmount = 50;

                GetComponent<AudioSource>().PlayOneShot(getmoney);
                PlayerPrefs.SetInt("WITgameO", PlayerPrefs.GetInt("WITgameO") + getMoneyAmount);
                CongText.text = "Congratulations!\n+ " + getMoneyAmount.ToString() + " coins!";
                PlayerPrefs.SetInt("LevelPassed" + gameController.ReloadLevel, 2);
                if (PlayerPrefs.GetInt("LevelPassed" + (gameController.ReloadLevel + 1)) != 2)
                    PlayerPrefs.SetInt("LevelPassed" + (gameController.ReloadLevel + 1), 1);
                if (gameController.ReloadLevel == 5)
                    PlayerPrefs.SetInt("LevelPassed" + 31, 1);
            }
            else
                CongText.text = "Congratulations!";
            
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.z > -30 && WinPanel.activeSelf == false)
            transform.position += new Vector3(0, 0, hazardMove.speed);
    }
}
