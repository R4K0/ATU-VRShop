using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

internal interface IStateLogic
{
    public void Run();
    public void Exit();
}

public abstract class State : IStateLogic
{
    public StateMachine Parent;
    public Action OnFinished;
    public abstract void Run();

    public virtual void Exit()
    {
        if(OnFinished != null)
            OnFinished.Invoke();
    }
}

public class GoHomeState : State
{
    public override void Run()
    {
        throw new NotImplementedException();
    }
}

public class MoveState : State
{
    private readonly AIShopper _shopper;
    private readonly Vector3 _target;
    [CanBeNull] private readonly Action _onFinish;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    public MoveState(AIShopper shopper, Vector3 target, [CanBeNull] Action onFinish = null)
    {
        _shopper = shopper;
        _target = target;
        _onFinish = onFinish;
    }

    public override void Run()
    {
        _shopper.GetComponent<Animator>().SetBool(IsWalking, true);
        _shopper.path.Move(_target, _onFinish);
    }

    public override void Exit()
    {
        base.Exit();
        _shopper.GetComponent<Animator>().SetBool(IsWalking, false);
    }
}

public class StateMachine
{
    [CanBeNull] public State CurrentState;

    public void ChangeState(State newState, Action finished = null)
    {
        var oldState = CurrentState;
        if (oldState != null)
        {
            oldState.Exit();
            oldState.Parent = null;
        }

        CurrentState = newState;
        CurrentState.Parent = null;
        CurrentState.OnFinished = finished;
        CurrentState.Run();
    }
}
