using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "plant", menuName = "Plants/New Plant")]
public class PlantDefinition : ScriptableObject
{
    public GameObject plantPrefab;
    public float timeToGrow;

    public String plantName;
    public List<GrowableSpot.SpotType> plantableSpots;
}