using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindState<T> : BasePathFinderState<T>
{
    public override void Enter()
    {
        base.Enter();
        OnStartMoving += SetStartPoint;
    }
    IMove mode;
    public PathFindState(Transform entity, IMove move, float minDistToTarget = 0.2F) : base(entity, minDistToTarget)
    {
        mode = move;
    }

    protected override void Move(Vector3 direction)
    {
        mode.Move(direction);
        mode.Look(direction);
    }

    private void SetStartPoint()
    {
        mode.SetPosition(nodes[0]);
    }
}
