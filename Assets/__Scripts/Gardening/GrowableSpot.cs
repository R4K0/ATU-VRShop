using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GrowableSpot : MonoBehaviour, IGrowableSpot
{
    public enum SpotType
    {
        Single,
        Small,
        Medium,
        Big
    }

    [Header("Spot Specific")]
    [SerializeField] private float growMultiplier = 1f;
    
    private PlantDefinition _plantDefinition;

    [SerializeField]
    private Transform attachSpot;
    
    [SerializeField]
    private SpotType spotType = SpotType.Single;
    
    private float _growthTime;
    private bool Grown => _plantObject != null;
    private GameObject _plantObject;

    public UnityEvent<float> growthUpdated;
    public UnityEvent<PlantDefinition> plantStatus;
    public PlantDefinition PlantDefinition
    {
        get => _plantDefinition;
        set
        {
            _growthTime = 0f;
            _plantDefinition = value;
            
            plantStatus.Invoke(value);

            if(value != null && _plantObject != null)
                Destroy(_plantObject);
        }
    }

    public void DetachPlant()
    {
        if (!Grown)
            return;
        
        if(!_plantObject.TryGetComponent<DetachableVegetable>(out var pickableVegetable))
            return;
        
        var rigidbodyComponent = _plantObject.GetComponent<Rigidbody>();
        if (rigidbodyComponent != null)
        {
            rigidbodyComponent.constraints = RigidbodyConstraints.None;
        }

        pickableVegetable.spot = null;
        _plantObject = null;
    }
    
    public virtual bool CanGrow()
    {
        if (Grown)
            return false;
        
        if (_plantDefinition == null)
            return false;

        if (_plantObject)
            return false;
        
        if (_growthTime >= _plantDefinition.timeToGrow)
        {
            OnFullGrown();
            return false;
        }

        return true;
    }

    public virtual void OnFullGrown()
    {
        if(_plantDefinition.plantPrefab != null)
            _plantObject = Instantiate(_plantDefinition.plantPrefab, attachSpot.transform.position, Quaternion.identity);

        if (_plantObject.TryGetComponent(out DetachableVegetable vegetable))
        {
            vegetable.spot = this;
        }
        
        PlantDefinition = null;
    }

    public virtual void Plant(PlantDefinition plantDefinition)
    {
        if(!CanPlant(plantDefinition))
            return;

        _plantDefinition = plantDefinition;
    }

    public virtual void DoGrow()
    {
        _growthTime += Time.deltaTime * growMultiplier;
        
        growthUpdated.Invoke(_growthTime);
    }

    public virtual bool CanPlant(PlantDefinition plantDefinition)
    {
        return plantDefinition.plantableSpots.Contains(spotType) && !Grown;
    }

    private void Update()
    {
        if (!CanGrow())
            return;

        DoGrow();
    }
}