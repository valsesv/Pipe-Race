using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesMotion : MonoBehaviour
{
    public GameObject pipe2, gme;
    private void Start()
    {
        pipe2.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
    }
    void FixedUpdate()
    {
        if (gme.activeSelf)
        {
            if (pipe2.GetComponent<MeshRenderer>().material != GetComponent<MeshRenderer>().material)
                pipe2.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
            if (transform.position.z >= -75.2 && transform.position.z <= 75)
            {
                transform.position += new Vector3(0, 0, hazardMove.speed);
                pipe2.transform.position = new Vector3(0, 0, transform.position.z + 150.2f);
            }
            else
            {
                pipe2.transform.position += new Vector3(0, 0, hazardMove.speed);
                transform.position = new Vector3(0, 0, pipe2.transform.position.z + 150.2f);
            }
        }
    }
}
