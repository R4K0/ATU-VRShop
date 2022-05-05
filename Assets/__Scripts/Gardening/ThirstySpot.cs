using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ThirstySpot : GrowableSpot, IThirstySpot, IHealthySpot
{
    [Header("Spot Thirst")]
    [SerializeField] private float water = 100f;
    [SerializeField] private float thirstRate = 1f;

    public float ActualThirstRate => (thirstRate + PlantDefinition.thirstMultiplayer) / 2f;
    
    public UnityEvent<float> waterUpdated;
    public UnityEvent<float> healthUpdated;
    
    [FormerlySerializedAs("_health")]
    [Header("Spot Health")]
    [SerializeField]
    private float health;
    [FormerlySerializedAs("_maxHealth")] [SerializeField]
    private float maxHealth;

    public float Health
    {
        get => health;
        private set
        {
            health = value;
            healthUpdated.Invoke(value);
        }
    }

    public float MaxHealth
    {
        get => maxHealth;
        private set => maxHealth = value;
    }
    public override bool CanGrow()
    {
        if (IsDead)
            return false;

        return base.CanGrow();
    }

    public void AddWater(float waterToAdd)
    {
        water = Math.Min(water + waterToAdd, 100);
    }

    public override void DoGrow()
    {
        if (water <= 0f)
        {
            Damage(5f * Time.deltaTime);
        } else {
            base.DoGrow();
            water -= Time.deltaTime * ActualThirstRate;
            waterUpdated.Invoke(water);
        }
    }

    public void Damage(float damage)
    {
        Health = Math.Max(0, Health - damage);

        if (IsDead)
        {
            PlantDefinition = null;
        }
    }

    public override void PostPlant()
    {
        base.PostPlant();

        Health = 100;
    }

    public override bool CanPlant(PlantDefinition plantDefinition)
    {
        if (water <= 0f)
            return false;
        
        return base.CanPlant(plantDefinition);
    }

    public bool IsDead => Health <= 0f;
    public bool IsAlive => !IsDead;

    public float HealthPercentage => Health / MaxHealth;
}

public interface IHealthySpot
{
    void Damage(float damage);
    
    bool IsDead { get; }
    
    bool IsAlive { get; }
    
    float HealthPercentage { get; }
}