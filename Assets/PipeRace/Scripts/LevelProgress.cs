using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    public GameObject Endzone;
    public float value;

    private void FixedUpdate()
    {
        if (gameController.ReloadLevel != 31)
        {
            value = (856.4608f - Endzone.transform.position.z) / 856.4608f;
        }
        else
        {
            value = scoreScript.score / PlayerPrefs.GetFloat("gameOasdfg");
        }
        GetComponent<Scrollbar>().size = value;
    }
}
