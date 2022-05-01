using UnityEngine;
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
    private SpotType spotType = SpotType.Single;
    
    private float _growthTime;
    private bool _grown;
    private GameObject _plantObject;

    public PlantDefinition PlantDefinition
    {
        get => _plantDefinition;
        set
        {
            _grown = false;
            _growthTime = 0f;
            _plantDefinition = value;
        }
    }

    public virtual bool CanGrow()
    {
        if (_grown)
            return false;
        
        if (_plantDefinition == null)
            return false;

        if (_plantObject != null)
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
        _plantObject = Instantiate(_plantDefinition.plantPrefab, transform);
        _grown = true;
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
    }

    public virtual bool CanPlant(PlantDefinition plantDefinition)
    {
        return plantDefinition.plantableSpots.Contains(spotType);
    }

    private void Update()
    {
        if (!CanGrow())
            return;

        DoGrow();
    }
}