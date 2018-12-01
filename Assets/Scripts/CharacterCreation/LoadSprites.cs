using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprites : MonoBehaviour
{
    public HeroController heroController;
    public ScriptableOutfit[] outfits;
    public GameObject hero;
    public GameObject damsel;

    private void Start()
    {
        int childObject = (HeroDetails.heroHeadValue + 1) * (HeroDetails.heroBodyValue + 1);
        hero.transform.GetChild((childObject -1)).gameObject.SetActive(true);
        heroController.anim = heroController.transform.GetChild((childObject - 1)).GetComponent<Animator>();
    }
}
