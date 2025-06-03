using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatePatrol : PathFindState<NPCStates>
{   
    public NPCStatePatrol(Transform entity, IMove move, Node start, Node goal, List<Vector3> waypoints = null, float distanceToPoint = 0.2f) : base(entity, move, start, goal, waypoints,distanceToPoint)
    {
        
    }

    public override void Enter()
    {
        OnDestinationReached += SwitchWaypoints;
        base.Enter();        
    }
    public override void FixedExecute()
    {
        base.FixedExecute();
    }

    public override void Exit()
    {
        base.Exit();
        OnDestinationReached -= SwitchWaypoints;
    }

    protected override float CalculateCost(Node parent, Node child)
    {
        float cost = base.CalculateCost(parent, child);

        if (child.hasEnemy)
        {
            cost -= 50;
        }

        return cost;
    }

    private void SwitchWaypoints()
    {
        var temp = _start;
        _start = _goal;
        _goal = temp;
    }
}
