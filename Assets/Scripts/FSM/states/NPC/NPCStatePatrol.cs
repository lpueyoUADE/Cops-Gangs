using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatePatrol : PathFindState<NPCStates>
{
    IMove move;
    IFoeDetection foeDetection;
    
    public NPCStatePatrol(Transform entity, IMove move, Node start, Node goal, List<Vector3> waypoints = null, float distanceToPoint = 0.2f) : base(entity, move, start, goal, waypoints,distanceToPoint)
    {
        
    }

    public override void Enter()
    {
        base.Enter();        
        OnDestinationReached += SwitchWaypoints;
        //OnDestinationReached += TestDestination;
    }

    private void TestDestination()
    {
        Debug.Log("Destination Reached");
        Debug.Log("Start:" + _start.name);
        Debug.Log("Goal:" + _goal.name);
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
    }

    private void SwitchWaypoints()
    {
        var temp = _start;
        _start = _goal;
        _goal = temp;
    }
}
