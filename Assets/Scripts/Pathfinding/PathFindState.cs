using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindState<T> : BasePathFinderState<T>
{
    //public override void Enter()
    //{
    //    base.Enter();
    //    OnStartMoving += SetStartPoint;
    //}
    //IMove mode;
    //public PathFindState(Transform entity, IMove move, float minDistToTarget = 0.2F) : base(entity, minDistToTarget)
    //{
    //    mode = move;
    //}

    //protected override void Move(Vector3 direction)
    //{
    //    mode.Move(direction);
    //    mode.Look(direction);
    //}  

    IMove _move;
    Animator _anim;
    public Node _start;
    public Node _goal;
    public Transform target;

    //public PathFindState(Transform entity, IMove move, float distanceToPoint = 0.2F) : base(entity, distanceToPoint)
    //{
    //    _move = move;
    //}
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
        SetPathDijkstra();
    }

    protected override void Move(Vector3 pos, Vector3 dir)
    {
        base.Move(pos, dir);
        _move.Move(pos);
        _move.Look(dir);
    }

    public void SetPath()
    {
        List<Node> path = BFS.Run<Node>(_start, IsSatisfies, GetConnections);
        //Debug.Log(path.Count);
        if (path.Count <= 0) return;
        SetNodes(GetPathVector(path));
    }

    public void SetPathDijkstra()
    {
        List<Node> path = BFS_Dijkstra.Run<Node>(_start, IsSatisfies, GetConnections, GetCost);
        //Debug.Log(path.Count);
        if (path.Count <= 0) return;
        SetNodes(GetPathVector(path));        
    }

    //public void SetPathAStar()
    //{
    //    var start = GetNearNode(_entity.position);
    //    goal = GetNearNode(target.position);
    //    List<Node> path = ASTAR.Run<Node>(start, IsSatisfies, GetConnections, GetCost, Heuristic);
    //    //Debug.Log(path.Count);
    //    if (path.Count <= 0) return;
    //    SetWaypoints(GetPathVector(path));
    //}

    //Node GetNearNode(Vector3 pos)
    //{
    //    var colls = Physics.OverlapSphere(pos, Constants.nearNodeDistance, Constants.nodeMask);
    //    Node nearNode = null;
    //    float nearDistance = 0;
    //    for (int i = 0; i < colls.Length; i++)
    //    {
    //        var currentNode = colls[i].GetComponent<Node>();
    //        if (currentNode == null) continue;

    //        var currentDistance = Vector3.Distance(currentNode.transform.position, pos);
    //        if (nearNode == null || nearDistance > currentDistance)
    //        {
    //            Vector3 dir = currentNode.transform.position - pos;
    //            if (Physics.Raycast(pos, dir.normalized, dir.magnitude, Constants.obsMask)) continue;

    //            nearNode = currentNode;
    //            nearDistance = currentDistance;
    //        }
    //    }
    //    return nearNode;
    //}

    //float Heuristic(Node node)
    //{
    //    float h = 0;
    //    h += Vector3.Distance(node.transform.position, goal.transform.position);
    //    return h;
    //}

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
