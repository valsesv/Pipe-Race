using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseanim : MonoBehaviour
{
    int c;
    public Animator anim;
    
    private void Awake()
    {
        c = 0;
    }

    void Update()
    {
        if (Time.timeScale == 1 && c == 0)
        {
            c++;
            anim.Play("continue");
        }
        else if (Time.timeScale != 1 && c == 1)
        {
            c--;
            anim.Play("pause");
        }
    
    }
}
