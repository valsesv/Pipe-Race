using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDown : MonoBehaviour
{
    private Vector3 Size;

    private void Start()
    {
        Size = this.transform.localScale;
    }
    void FixedUpdate()
    {
        if (transform.position.z < 15 && this.transform.localScale.z > 0)
        {
            this.transform.localScale -= Size / 30;
            if (this.transform.localScale.z <= 0)
                this.transform.localScale = Vector3.zero;
        }

    }
}
