using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Outfit")]
public class ScriptableOutfit : ScriptableObject
{
    [Header("Assigned head number for sprite")]
    [Tooltip("value should be consistent for same head sprite across all scriptables")]
    public int headNum; 
    [Header("Assigned body number for sprite")]
    [Tooltip("value should be consistent for same body sprite across all scriptables")]
    public int bodyNum;
    [Header("Outfit Sprite")]
    public Sprite sprite;
}
