using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

	void FixedUpdate ()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 1)
        {
            transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
        }
	}
}
