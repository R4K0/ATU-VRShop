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
        private set => health = value;
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

        if (water <= 0f)
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
            Damage(20f * Time.deltaTime);
        } else {
            base.DoGrow();
            water -= Time.deltaTime * ActualThirstRate;
            waterUpdated.Invoke(water);
        }
    }

    public void Damage(float damage)
    {
        Health = Math.Max(0, Health - damage);
        healthUpdated.Invoke(Health);

        if (IsDead)
        {
            PlantDefinition = null;
        }
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