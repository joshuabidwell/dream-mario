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
        Debug.Log(HeroDetails.name);
        Debug.Log(GameObject.Find(HeroDetails.name));
        damsel.GetComponent<SpriteRenderer>().sprite = DamselDetails.damsel;
        foreach (Transform child in hero.transform)
        {
            if (child.name != HeroDetails.name)
            {
                child.gameObject.SetActive(false);
            }
        }
        heroController.anim = GameObject.Find(HeroDetails.name).GetComponent<Animator>();
    }
}
