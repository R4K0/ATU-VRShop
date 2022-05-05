using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIShopper : MonoBehaviour
{
    private StateMachine _stateMachine;

    public AIPathNavigator path;
    public Transform targetTable;
    public Transform targetHome;
    public ProductTracker tracker;
    public PopulationManager populationManager;

    private void OnDestroy()
    {
        if(populationManager != null)
            populationManager.InformDestroy(gameObject);
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        
        if (path == null)
            path = GetComponent<AIPathNavigator>();
        
        _stateMachine.ChangeState(new MoveState(this, targetTable.position, () =>
        {
            // This is inside of the finish callback for reaching the table, we'll interact with the table after.
            tracker.ProcessNpcPurchase();
            
            // We just chained two state changes using callbacks, nice :)
            _stateMachine.ChangeState(new MoveState(this, targetHome.position, () =>
            {
                Destroy(gameObject);
            }));
        }));
    }
}
