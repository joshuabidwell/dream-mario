using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public bool right;

    private void Start()
    {
    }

    void Update ()
    {
        if (right == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, (1 * Time.fixedDeltaTime));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, (1 * Time.fixedDeltaTime));
        }
        if (Vector3.Distance(transform.position, pointA) < 1)
        {
            right = true;
        }
        if (Vector3.Distance(transform.position, pointB) < 1)
        {
            right = false;
        }

    }
}
