using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    public CharacterDetails[] characters;
    public ScriptableOutfit[] possibleCharacterOutfits;
    public AudioClip[] possibleCharacterVoices;
    public AudioSource audioSource;

    void Awake()
    {
        StartRandom();
    }

    public void ModifyHeroHead(int newValue)
    {
        audioSource.Play();
        if ((HeroDetails.heroHeadValue + newValue) >= 0 && (HeroDetails.heroHeadValue + newValue) <= 3)
        {
            HeroDetails.heroHeadValue += newValue;
        }
        else
        {
            if ((HeroDetails.heroHeadValue + newValue) < 0)
            {
                HeroDetails.heroHeadValue = 3;
            }
            if ((HeroDetails.heroHeadValue + newValue) > 3)
            {
                HeroDetails.heroHeadValue = 0;
            }
        }
        HeroOutfit();
    }

    public void ModifyHeroBody(int newValue)
    {
        audioSource.Play();
        if ((HeroDetails.heroBodyValue + newValue) >= 0 && (HeroDetails.heroBodyValue + newValue) <= 3)
        {
            HeroDetails.heroBodyValue += newValue;
        }
        else
        {
            if ((HeroDetails.heroBodyValue + newValue) < 0)
            {
                HeroDetails.heroBodyValue = 3;
            }
            if ((HeroDetails.heroBodyValue + newValue) > 3)
            {
                HeroDetails.heroBodyValue = 0;
            }
        }
        HeroOutfit();
    }

    public void ModifyHeroVoice(int newValue)
    {
        audioSource.Play();
        if ((HeroDetails.heroVoiceValue + newValue) >= 0 && (HeroDetails.heroVoiceValue + newValue) <= 3)
        {
            characters[0].voice.clip = possibleCharacterVoices[HeroDetails.heroVoiceValue];
        }
        else
        {
            if ((HeroDetails.heroVoiceValue + newValue) < 0)
            {
                characters[0].voice.clip = possibleCharacterVoices[3];
            }
            if ((HeroDetails.heroVoiceValue + newValue) > 3)
            {
                characters[0].voice.clip = possibleCharacterVoices[0];
            }
        }
        DamselVoice();
    }

    public void HeroOutfit()
    {
        foreach (ScriptableOutfit outfit in possibleCharacterOutfits)
        {
            if (outfit.headNum == HeroDetails.heroHeadValue && (outfit.bodyNum == HeroDetails.heroBodyValue))
            {
                characters[0].outfit.sprite = outfit.sprite;
                HeroDetails.name = outfit.sprite.name;
                DamselOutfit();
            }
        }
    }

    public void DamselOutfit()
    {
        int damselHeadValue = (Random.Range(0, 3));
        int damselBodyValue = (Random.Range(0, 3));

        foreach (ScriptableOutfit outfit in possibleCharacterOutfits)
        {
            if (outfit.headNum == damselHeadValue && outfit.bodyNum == damselBodyValue)
            {
                DamselDetails.damsel = outfit.sprite;
            }

        }
    }

    public void DamselVoice()
    {
        int damselVoiceValue = (Random.Range(0, 3));
        DamselDetails.damselVoiceValue = damselVoiceValue;
    }


    public void StartRandom()
    {
        int randomOutfit = Random.Range(0, (possibleCharacterOutfits.Length - 1));
        int randomVoice = Random.Range(0, (possibleCharacterVoices.Length - 1));
        SetHeroDetails(randomOutfit, randomVoice);
    }

    public void SetHeroDetails(int newOutfit, int newVoice)
    {
        HeroDetails.heroBodyValue = possibleCharacterOutfits[newOutfit].bodyNum;
        HeroDetails.heroHeadValue = possibleCharacterOutfits[newOutfit].headNum;
        HeroDetails.heroVoiceValue = newVoice;
        HeroOutfit();
        ModifyHeroVoice(0);
    }
}
