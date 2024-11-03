using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindState<T> : BasePathFinderState<T>
{
    IMove _move;
    Animator _anim;
    public Node _start;
    public Node _goal;
    public Transform target;

    public PathFindState(Transform entity, IMove move, Node start, Node goal, List<Vector3> waypoints = null, float distanceToPoint = 0.2f) : base(entity, distanceToPoint)
    {
        _move = move;
        _start = start;
        _goal = goal;
    }    

    public override void Enter()
    {
        Debug.Log("Pathfinding");
        base.Enter();
        SetPath();
    }

    protected override void Move(Vector3 pos, Vector3 dir)
    {
        base.Move(pos, dir);
        _move.Move(pos);
        _move.Look(dir);
    }   

    public void SetPath()
    {
        List<Node> path = AStar.Run<Node>(_start, IsSatisfies, GetConnections, GetCost, GetHeuristic);
        //Debug.Log(path.Count);
        if (path.Count <= 0) return;
        SetNodes(GetPathVector(path));
    }

    float GetHeuristic(Node node)
    {
        float h = 0;
        h += Vector3.Distance(node.transform.position, _goal.transform.position);
        return h;
    }

    float GetCost(Node parent, Node child)
    {
        float multiplierDistance = 1;

        float cost = 0;
        cost += Vector3.Distance(parent.transform.position, child.transform.position) * multiplierDistance;

        if (child.hasObstacle)
        {
            cost += 100;
        }
        return cost;
    }
    List<Vector3> GetPathVector(List<Node> path)
    {
        List<Vector3> pathVector = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            pathVector.Add(path[i].transform.position);
        }
        return pathVector;
    }
    bool IsSatisfies(Node current)
    {
        return current == _goal;
    }
    List<Node> GetConnections(Node current)
    {
        return current.Neighbours;
    }

    public void SetStartPoint()
    {
        _move.SetPosition(nodes[0]);
    }
}
