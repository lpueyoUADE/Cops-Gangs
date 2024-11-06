using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindState<T> : BasePathFinderState<T>
{
    IMove _move;
    Animator _anim;
    protected Node _start;
    protected Node _goal;
    protected Transform target;

    public PathFindState(Transform entity, IMove move, Node start, Node goal, List<Vector3> waypoints = null, float distanceToPoint = 0.2f) : base(entity, distanceToPoint)
    {
        _move = move;
        _start = start;
        _goal = goal;
    }    

    public override void Enter()
    {
        base.Enter();
        SetPath();        
    }

    protected override void Move(Vector3 pos)
    {
        base.Move(pos);
        _move.Move(pos);
        _move.Look(pos);
    }
    protected override void Move(Vector3 pos, Vector3 dir)
    {
        base.Move(pos, dir);
        _move.Move(pos);
        _move.Look(dir);
    }

    public void SetPath()
    {
        List<Node> path = AStar.Run<Node>(_start, CheckForGoal, GetNeighbours, CalculateCost, CalculateHeuristic);
        //Debug.Log(path.Count);
        if (path.Count <= 0) return;
        SetNodes(BuildPath(path));
    }

    protected virtual float CalculateHeuristic(Node node)
    {
        float h = 0;
        h += Vector3.Distance(node.transform.position, _goal.transform.position);
        return h;
    }

    protected virtual float CalculateCost(Node parent, Node child)
    {
        float cost = 0;
        cost += Vector3.Distance(parent.transform.position, child.transform.position);

        if (child.hasObstacle)
        {
            cost += 100;
        }        
        
        return cost;
    }
    private List<Vector3> BuildPath(List<Node> path)
    {
        List<Vector3> newPath = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            newPath.Add(path[i].transform.position);
        }
        return newPath;
    }
    protected virtual bool CheckForGoal(Node current)
    {
        return current == _goal;
    }
    protected virtual List<Node> GetNeighbours(Node current)
    {
        return current.Neighbours;
    }
}
