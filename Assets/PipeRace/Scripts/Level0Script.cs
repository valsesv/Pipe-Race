using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level0Script : MonoBehaviour
{
    public List<GameObject> BeginingObjects = new List<GameObject>();
    public GameObject LevelEnd, pauseButton, player;
    public Text WhatToDo;
    public gameController LevelLoad;
    public static bool IsLost;
    float TappedTime;
    bool Tap;
    private void Awake()
    {
        Tap = false;
        TappedTime = 0;
        LevelEnd.transform.position = new Vector3(109.6399f, 739.9585f, 205);
        IsLost = false;
        //if (PlayerPrefs.GetInt("HowToDO") == 1)
        //{
            WhatToDo.text = "tap and hold";
        //}
        //else
        //    WhatToDo.text = "rotate device";
        
        BeginingObjects[3].SetActive(true);
        if (PlayerPrefs.GetInt("LevelPassed" + 0) == 1)
            pauseButton.SetActive(false);
    }
    private void Start()
    {
        BeginingObjects[2].SetActive(true);
    }

    public void OnTap(bool Tapped)
    {
        Tap = Tapped;
    }

    private void Update()
    {
        if (IsLost == true)
            LevelLoad.ReplayGame();
        if (Tap)
            TappedTime += Time.deltaTime;
        else
            TappedTime = 0;
        if (BeginingObjects[2].activeSelf == true && TappedTime > 1)
        {
            BeginingObjects[2].SetActive(false);
            BeginingObjects[1].SetActive(true);
            Tap = false;
            TappedTime = 0;
        }
        if (BeginingObjects[1].activeSelf == true && TappedTime > 1)
        {
            BeginingObjects[1].SetActive(false);
            BeginingObjects[0].SetActive(false);
            BeginingObjects[3].SetActive(false);
            Tap = false;
        }
    }

    private void FixedUpdate()
    {
        if (BeginingObjects[3].activeSelf == true) {
            transform.position -= new Vector3(0, 0, hazardMove.speed);
            LevelEnd.transform.position -= new Vector3(0, 0, hazardMove.speed);
        }
    }
}
