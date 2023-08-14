using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotScript : MonoBehaviour
{
    public int side = 1;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, side * (moveScript.speed - 0.5f)));
    }
}
