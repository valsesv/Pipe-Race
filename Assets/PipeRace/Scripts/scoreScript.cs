using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public Text ScoreText;
    public AudioClip hazard, coin;
    public GameObject Endlees;
    public static float score;
    private float kf;
    public int th;

    private void Start()
    {
        th = 1;
        score = 0;
        kf = 0.3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(hazard);
        Destroy(other.gameObject);
    }

    private void FixedUpdate()
    {
            if (Time.timeScale == 1 && Endlees.activeSelf == true)
            {
                score += -hazardMove.speed * kf;
                kf += 0.0003f;
                if ((int)score / 500 == th)
                {
                    PlayerPrefs.SetInt("WITgameO", PlayerPrefs.GetInt("WITgameO") + th * 10);
                    GetComponent<AudioSource>().PlayOneShot(coin);
                    th++;
                }
                ScoreText.text = score.ToString("0");
            if (score > PlayerPrefs.GetFloat("max Score"))
            {
                PlayerPrefs.SetFloat("gameOasdfg", score);
            }
            }
            else if (gameController.ReloadLevel != 31)
                ScoreText.text = "Level " + gameController.ReloadLevel;
    }

}
