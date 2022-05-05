using System;
using JetBrains.Annotations;
using Pathfinding;
using UnityEngine;

public class AIPathNavigator : AIPath
{
    [CanBeNull] public Action Reached;
    private Animator _animator;

    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
    }
    
    public void Move(Vector3 destination, [CanBeNull] Action onReach)
    {
        this.destination = destination;
        Reached = onReach;
    }
    
    public override void OnTargetReached()
    {
        base.OnTargetReached(); 
        
        Reached?.Invoke();
    }
}