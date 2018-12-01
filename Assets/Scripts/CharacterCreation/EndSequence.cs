using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSequence : MonoBehaviour
{
    [Header("OutFits")]
    public ScriptableOutfit[] outfits;
    [Header("Characters")]
    public GameObject hero;
    public GameObject damsel;

    #region hideInInspector
    [HideInInspector]public Camera mainCamera;
    #endregion

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        foreach (ScriptableOutfit outfit in outfits)
        {
            if (outfit.headNum == HeroDetails.heroHeadValue && (outfit.bodyNum == HeroDetails.heroBodyValue))
            {
                hero.GetComponent<SpriteRenderer>().sprite = outfit.sprite;
            }
        }
        //damsel.GetComponent<SpriteRenderer>().sprite = HeroDetails.damsel;
        StartCoroutine("EndEvent");
    }

    public void Update()
    {
        if (Vector3.Distance(hero.transform.position, mainCamera.transform.position) <= 3.9)
        {
            mainCamera.transform.parent = hero.transform;
        }
    }

    public IEnumerator EndEvent()
    {
       // LeanTween.move(hero, new Vector3((hero.transform.position.x + 15), hero.transform.position.y, hero.transform.position.z), 8);
        yield return new WaitForSeconds(8);
    }

}
