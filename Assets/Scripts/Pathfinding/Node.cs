using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> Neighbours;

    public bool hasBonus;

    public bool hasObstacle;

    private void OnDrawGizmos()
    {
        foreach(var node in Neighbours)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
        
    }


}
