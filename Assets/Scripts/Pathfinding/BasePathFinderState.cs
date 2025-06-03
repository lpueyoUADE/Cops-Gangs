using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePathFinderState <T> : State<T>
{
    protected List<Vector3> nodes;
    int index;
    Transform user;
    float AcceptableDistanceToTarget;
    bool destinationReached = true;
    public bool DestinationReached { get { return destinationReached; } }

    public System.Action OnDestinationReached = delegate { };
    public System.Action OnStartMoving = delegate { };

    public BasePathFinderState(Transform entity, float minDistToTarget = 0.2f)
    {
        user = entity;
        AcceptableDistanceToTarget = minDistToTarget;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        Run();
    }

    public void Run()
    {
        if (!destinationReached)
        {
            Vector3 currentTargetNode = nodes[index];
            Vector3 direction = currentTargetNode - user.position;

            if (direction.magnitude <= AcceptableDistanceToTarget)
            {
                if (index + 1 < nodes.Count)
                {
                    index++;
                }
                else
                {
                    OnDestinationReached();
                    destinationReached = true;
                    return;
                }
            }
            else
            {
                Move(direction.normalized);
            }
        }
        else
        {
            return;
        }
        
    }

    protected virtual void Move(Vector3 pos)
    {

    }
    protected virtual void Move(Vector3 pos, Vector3 dir)
    {

    }

    public void SetNodes(List<Vector3> newNodes)
    {
        if (newNodes.Count == 0)
        {
            return;
        }
        else
        {
            nodes = newNodes;
            index = 0;
            destinationReached = false;
            OnStartMoving();
        }
        
    }
}
