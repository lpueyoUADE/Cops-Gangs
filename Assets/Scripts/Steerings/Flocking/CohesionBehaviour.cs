using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehaviour : MonoBehaviour, IFlockingBehaviour
{
    public float multiplier;

    public EntityModel Leader => gameObject.GetComponent<FollowerModel>().Leader;

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        Vector3 center = Vector3.zero;
        Vector3 cohesion = Vector3.zero;

        for (int i = 0; i < boids.Count; i++)
        {
            center += boids[i].Position;
        }

        if (boids.Count > 0)
        {
            center /= boids.Count;
            cohesion = center - self.Position;
        }

        return cohesion.normalized * multiplier;
    }
}
