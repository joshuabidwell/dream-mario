using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprites : MonoBehaviour
{
    public ScriptableOutfit[] outfits;
    public GameObject hero;
    public GameObject damsel;

    private void Start()
    {
        int childObject = (HeroDetails.heroHeadValue + 1) * (HeroDetails.heroBodyValue + 1);
        hero.transform.GetChild((childObject -1)).gameObject.SetActive(true);  
    }
}
