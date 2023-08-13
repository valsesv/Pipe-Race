using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardMove : MonoBehaviour
{
    public static float speed;

    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, speed);
    }
}
