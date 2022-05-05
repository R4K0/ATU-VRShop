using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopulationManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform[] homePoints;

    public Transform tableTarget;
    public ProductTracker trackerProduct;

    [Range(0, 10)]
    public int maxPopulation = 6;

    [Range(10, 1000)]
    public int spawnInterval = 35;

    public GameObject npcPrefab;


    private List<GameObject> AIs = new List<GameObject>();


    private Coroutine _spawnerCoroutine;

    private void OnEnable()
    {
        _spawnerCoroutine = StartCoroutine(SpawnAI());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnerCoroutine);
    }


    private IEnumerator SpawnAI()
    {
        if(!enabled)
            yield break;

        if (CanSpawn())
        {
            InstantiateAI();
            
            yield return new WaitForSeconds(Random.Range(10, spawnInterval));
        }
        else
        {
            yield return new WaitForSeconds(20);
        }

        yield return SpawnAI();
    }

    public void InformDestroy(GameObject ai)
    {
        AIs.Remove(ai);
    }
    
    private bool CanSpawn()
    {
        return AIs.Count < maxPopulation;
    }
    
    private void InstantiateAI()
    {
        var randomSpawn = GetRandomSpawn();

        var aiObject = Instantiate(npcPrefab, randomSpawn.position, Quaternion.identity);
        var aiShopper = aiObject.GetComponent<AIShopper>();
        
        aiShopper.tracker = trackerProduct;
        aiShopper.targetHome = GetRandomHome();
        aiShopper.targetTable = tableTarget;
        aiShopper.populationManager = this;
        
        AIs.Add(aiObject);
    }

    private Transform GetRandomSpawn()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
    
    private Transform GetRandomHome()
    {
        return homePoints[Random.Range(0, homePoints.Length)];
    }
}
