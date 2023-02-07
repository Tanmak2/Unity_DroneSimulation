using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    Vector3 cam;
    void Start()
    {
        cam = new Vector3(0, 2, -2);
    }

    void Update()
    {
        transform.position = target.transform.position + cam;
    }
}
