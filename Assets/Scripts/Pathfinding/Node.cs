using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> Neighbours;

    public bool hasObstacle;

    [SerializeField] LayerMask obstacle;

    private void Start()
    {
        if (Physics.CheckSphere(transform.position, 1, obstacle))
        {
            hasObstacle = true;
        }
    }

    private void OnDrawGizmos()
    {        
        foreach(Node node in Neighbours)
        {
            if (node.Neighbours.Contains(this))
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }            
        }
    }

}
