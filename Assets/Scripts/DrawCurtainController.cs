using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCurtainController : MonoBehaviour
{
    public float drawTime = 0.5f;
    public GameObject curtain;

    public void DrawCurtain()
    {
        if(curtain.activeInHierarchy == false)
            StartCoroutine(Draw());
    }

    IEnumerator Draw()
    {
        curtain.gameObject.SetActive(true);
        yield return new WaitForSeconds(drawTime);
        curtain.gameObject.SetActive(false);
    }
}
