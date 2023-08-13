using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevel : MonoBehaviour
{
    public List<GameObject> hazards = new List<GameObject>();
    public List<GameObject> ChillHazards = new List<GameObject>();
    public List<GameObject> Effect = new List<GameObject>();
    public GameObject newob;
    public Transform Hazards;
    Vector3 spawnPosition;
    public int leftgr, rightgr, Chill;
    private int lastg1 = -1, lastg2 = -1;

    void Start()
    {
        leftgr = 0; rightgr = 5;
        spawnPosition = new Vector3(0, 0, 360);
        for (int i = 0; i < 2; i++)
        {
            float RoAngle = 22.5f * Random.Range(0, 17);
            Instantiate(hazards[FindNum()], new Vector3(0, 0, 100 + i * 120), Quaternion.Euler(0, 0, RoAngle), Hazards);
        }
        Chill = Random.Range(3, 6);
        newob.SetActive(true);
        StartCoroutine(Spawn());
    }
    public int Was;
    private IEnumerator Spawn()
    {
        Was = 0;
        while (true)
        {
            float RoAngle = 22.5f * Random.Range(0, 17);

            if (Chill-- > 0)
            {
                Instantiate(hazards[FindNum()], spawnPosition, Quaternion.Euler(0, 0, RoAngle), Hazards);
                if (Random.Range(0, 3) == 1 && Was < 2)
                {
                    GameObject ok = Instantiate(Effect[Random.Range(0, Effect.Count)], new Vector3(0, 0, 350), Quaternion.Euler(0, 0, RoAngle), Hazards);
                    float tm = 2.5f;// * Random.Range(1, 3);
                    ok.GetComponent<GameEffects>().EffectTime = tm / -hazardMove.speed;
                    Was++;
                }
            Changegr();
            }
            else
            {
                Chill = Random.Range(4, 8);
                Instantiate(ChillHazards[Random.Range(0, ChillHazards.Count)], spawnPosition, Quaternion.Euler(0, 0, RoAngle), Hazards);
                Was = 0;
            }

            newob.transform.position = spawnPosition;
            while (newob.transform.position.z > 220)
                yield return new WaitForSeconds(0.05f);

            hazardMove.speed -= 0.005f;
            moveScript.speed += 0.013f;
        }
    }

    private void Changegr() {
        if (rightgr < hazards.Count)
        {
            rightgr += 2;
        }
        if (hazards.Count - leftgr > 15)
        {
            leftgr += 2;
            if (rightgr > 15 && hazards.Count - leftgr > 15)
                leftgr++;
        }
    }
    private int FindNum()
    {
        int g;
        do
        {
            g = Random.Range(leftgr, rightgr);
        } while (g == lastg1 || g == lastg2);

        lastg2 = lastg1;
        lastg1 = g;
        return g;
    }
}
