using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMaterial : MonoBehaviour
{
    public List<Material> HaColor = new List<Material>();
    public static int c;
    int ok;
    private void Awake()
    {
        c = ok = PlayerPrefs.GetInt("CNNumGameO");
        /*
        while (c == PlayerPrefs.GetInt("CNNumGameO"))
            c = Random.Range(0, HaColor.Count - 1);
            if (HaColor.Count > 0)
        */
        ChangeSkin(ok);
    }

    public void ChangeSkin(int num)
    {
        GetComponent<MeshRenderer>().material = HaColor[num];
    }

    private void Update()
    {
        if (c != ok)
        {
            ok = c;
            ChangeSkin(ok);
        }
    }
}
