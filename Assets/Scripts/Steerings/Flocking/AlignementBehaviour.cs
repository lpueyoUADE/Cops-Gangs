using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignementBehaviour : MonoBehaviour, IFlockingBehaviour
{
    public float multiplier;

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        Vector3 alignement = Vector3.zero;
        for (int i = 0; i < boids.Count; i++)
        {
            alignement += boids[i].Forward;
        }
        return alignement.normalized * multiplier;
    }
}
