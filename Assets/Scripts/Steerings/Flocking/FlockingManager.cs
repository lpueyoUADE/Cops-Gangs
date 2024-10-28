using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour, ISteering
{
    public int maxBoids; //cantidad màxima que se puede detectar
    public float radius;
    Collider[] cols;
    public LayerMask maskBoids;
    List<IBoid> boids;
    IBoid self;
    IFlockingBehaviour[] behaviours;

    private void Awake()
    {
        cols = new Collider[maxBoids];
        self = GetComponent<IBoid>();
        behaviours = GetComponents<IFlockingBehaviour>();
        boids = new List<IBoid>(maxBoids);
    }

    public Vector3 GetDir()
    {
        boids.Clear();
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, cols, maskBoids);
        for (int i = 0; i < count; i++)
        {
            var boid = cols[i].GetComponent<IBoid>();
            if (boid != null || boid == self) continue;
            boids.Add(boid);
        }

        Vector3 dir = Vector3.zero;
        for (int i = 0; i < behaviours.Length; i++)
        {
            dir += behaviours[i].GetDir(boids, self);
        }

        return dir.normalized;
    }
}
