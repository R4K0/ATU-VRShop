using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SeedBag : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject prefabToSpawn;

    public void SpawnSeed(SelectEnterEventArgs args)
    {
        print("Interacted with seed bag");
        
        if (!(args.interactorObject is XRDirectInteractor)) return;
        
        var spawnedPrefab = Instantiate(prefabToSpawn, spawnPoint.transform.position, Quaternion.identity);
        
        args.manager.SelectEnter(args.interactorObject, spawnedPrefab.GetComponent<XRGrabInteractable>());
    }
}
