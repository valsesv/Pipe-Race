using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUp : MonoBehaviour
{
    private Vector3 Size;

    private void Start()
    {
            Size = this.transform.localScale;
            this.transform.localScale = Vector3.zero;
    }
    void FixedUpdate()
    {
        if (transform.position.z < 31 && this.transform.localScale.z < Size.z)
        {
            this.transform.localScale += Size / 30;
            if (this.transform.localScale.z >= Size.z)
                this.transform.localScale = Size;
        }

    }
}
