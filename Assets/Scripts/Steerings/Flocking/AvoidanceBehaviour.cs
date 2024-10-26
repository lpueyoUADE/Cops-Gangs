using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceBehaviour : MonoBehaviour, IFlockingBehaviour
{
    public float multiplier;
    public float personalArea;

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        Vector3 avoidance = Vector3.zero;
        for (int i = 0; i < boids.Count; i++)
        {
            Vector3 diff = self.Position - boids[i].Position;
            float distance = diff.magnitude;

            if (distance > personalArea) continue;
            avoidance += diff.normalized * (personalArea - distance);
        }

        return avoidance.normalized * multiplier;
    }
}
