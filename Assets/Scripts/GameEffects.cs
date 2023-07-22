using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEffects : MonoBehaviour
{
    public string EffectType;
    public float EffectTime;
    public ShopScript shop;
    public float TimeUp;
    public AudioClip EffectSound;
    public List<GameObject> FreeEffects = new List<GameObject>();
    public List<Image> EffectImage = new List<Image>();
    public Sprite ImageFromAsset;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(EffectSound);
            if (EffectType == "Teleport")
            {
                Teleport(other.gameObject);
            }
            else
                StartCoroutine(ChangeSize());
            if (EffectType == "Inversion")
            {
                StartCoroutine(StopEffect());
            }
            if (EffectType == "SpeedUp")
            {
                StartCoroutine(SpeedUp());
            }
            if (EffectType == "ButtonSwap")
            {
                StartCoroutine(ButtonSwap());
            }
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator StopEffect()
    {
        //StartCoroutine(TimeLaps());
        int smth;
        do
        {
            smth = Random.Range(0, 11);
        } while (PlayerPrefs.GetInt("CNNumGameO") == smth);
        shop.skin(smth);
        HazardMaterial.c = smth;
        yield return new WaitForSeconds(EffectTime);
        shop.skin(PlayerPrefs.GetInt("CNNumGameO"));
        HazardMaterial.c = PlayerPrefs.GetInt("CNNumGameO");
        //StartCoroutine(TimeLaps());
    }

    void Teleport(GameObject Player)
    {
        Player.transform.RotateAround(Vector3.zero, Vector3.forward, 180);
        /*StartCoroutine(TimeLaps());
        for (int i = 0; i < 36; i++)
        {
            Player.transform.RotateAround(Vector3.zero, Vector3.forward, 5);
            yield return new WaitForSeconds(Time.timeScale / 72);
        }*/
    }

    IEnumerator SpeedUp()
    {
        hazardMove.speed *= TimeUp;
        moveScript.speed *= TimeUp;
        yield return new WaitForSeconds(EffectTime);
        hazardMove.speed /= TimeUp;
        moveScript.speed /= TimeUp;
    }

    IEnumerator ButtonSwap() {
        //StartCoroutine(TimeLaps());
        moveScript.SwapMotion = -1;
        yield return new WaitForSeconds(EffectTime);
        //StartCoroutine(TimeLaps());
        moveScript.SwapMotion = 1;
    }

    IEnumerator TimeLaps()
    {
        float timew = Time.timeScale;
        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(Time.timeScale);

        Time.timeScale = timew;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(EffectTime + 1);
        Destroy(gameObject);
    }
    float Size;
    int EffectNum;
    IEnumerator ChangeSize()
    {
        for (int i = 0; i < FreeEffects.Count; i++)
            if (!FreeEffects[i].activeSelf)
            {
                EffectNum = i;
                Size = EffectTime;
                FreeEffects[i].SetActive(true);
                EffectImage[i].sprite = ImageFromAsset;
                yield return new WaitForSeconds(EffectTime);
                FreeEffects[i].SetActive(false);
                break;
            }
    }

    private void FixedUpdate()
    {
        if (Size > 0)
        {
            Size -= Time.deltaTime;
            FreeEffects[EffectNum].GetComponent<Scrollbar>().size = Size / EffectTime;
        }
    }
}
