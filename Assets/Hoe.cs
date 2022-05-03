using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hoe : MonoBehaviour
{
    [Header("Constraints")]
    [SerializeField] private float minimumForce = 15f;
    [SerializeField] private Transform hitPosition;
    [SerializeField] private float occupancyCheckRadius = 10f;
    [SerializeField] private LayerMask countMask;

    [Header("Mud Setup")]
    [SerializeField] private GameObject pilePrefab;

    [Header("Hoe Setup")] [SerializeField] private ParticleSystem hitParticleSystem;

    private XRGrabInteractable _interactable;

    private const float ContactDistance = 3f;

    private void Start()
    {
        _interactable = GetComponent<XRGrabInteractable>();
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPosition)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(hitPosition.position, occupancyCheckRadius + ContactDistance);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(hitPosition.position, ContactDistance);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_interactable.isSelected)
            return;
        
        var contactPoint = collision.GetContact(0);
        if (!contactPoint.otherCollider.gameObject.CompareTag("DiggableTerrain"))
            return;
        
        print("Passed tag test");
        if (collision.relativeVelocity.magnitude <= minimumForce)
            return;

        if (Vector3.Distance(contactPoint.point, hitPosition.position) > ContactDistance)
            return;
        
        hitParticleSystem.Play();
        
        var colliders = Physics.OverlapSphere(contactPoint.point, occupancyCheckRadius, countMask);
        if (colliders.Length > 0)
            return;

        Instantiate(pilePrefab, contactPoint.point, Quaternion.identity);
    }
}
