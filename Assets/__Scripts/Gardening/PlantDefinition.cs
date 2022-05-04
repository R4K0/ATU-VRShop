using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "plant", menuName = "Plants/New Plant")]
public class PlantDefinition : ScriptableObject
{
    public GameObject plantPrefab;
    public GameObject seedPrefab;
    
    public float timeToGrow;
    public float thirstMultiplayer = 1f;

    public int sellPrice = 15;
    public int buyPrice = 4;
    public string plantName;
    public List<GrowableSpot.SpotType> plantableSpots;
}