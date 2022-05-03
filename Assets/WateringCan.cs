using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class WateringCan : MonoBehaviour
{
    [SerializeField] private WaterParticle particle;
    private XRGrabInteractable _interactable;

    private void Start()
    {
        _interactable = GetComponent<XRGrabInteractable>();
        
        particle.Toggle(false);
    }

    private void Update()
    {
        if (!_interactable.isSelected)
        {
            if(particle.IsFlowing)
                particle.Toggle(false);

            return;
        }

        
        particle.Toggle(transform.up.y < 0.6);
    }
}
