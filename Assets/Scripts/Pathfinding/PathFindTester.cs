using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindTester : MonoBehaviour, IMove
{
    [SerializeField] Node target;
    [SerializeField] Node start;
    private Queue<Node> path = new Queue<Node>();

    private Node currentDestination;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        var temp = BFS.Run<Node>(start, CheckForGoal, GetNeighbours);
        foreach (var node in temp)
        {
            path.Enqueue(node);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count > 0 && !moving)
        {
            currentDestination = path.Dequeue();            
            moving = true;
        }       

        if (moving)
        {
            //transform.Translate(currentDestination.transform.position);
        }

        if (transform.position == currentDestination.transform.position)
        {
            moving = false;
        }
        
    }

    public void Look(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public void Look(Transform target)
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public void SetPosition(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    bool CheckForGoal(Node currentNode)
    {
        return currentNode == target;
    }

    List<Node> GetNeighbours(Node node)
    {
        return node.Neighbours;
    }
}
