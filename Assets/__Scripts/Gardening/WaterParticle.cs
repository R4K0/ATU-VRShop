using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;

    private readonly List<ParticleCollisionEvent> _events = new List<ParticleCollisionEvent>();

    public bool IsFlowing => system.isPlaying;
    private void OnParticleCollision(GameObject other)
    {
        var waterSpot = other.GetComponentInParent<ThirstySpot>();
        if (!waterSpot) return;
        
        var number = system.GetCollisionEvents(other, _events);
        waterSpot.AddWater(number * 0.5f);
    }

    public void Toggle(bool state)
    {
        switch (state)
        {
            case false when system.isPlaying:
                system.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                break;
            case true when !system.isPlaying:
                system.Play();
                break;
        }
    }
}
