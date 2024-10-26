using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour, ISteering
{
    public int maxBoids; //cantidad màxima que se puede detectar
    public float radius;
    Collider[] cols;
    LayerMask maskBoids;
    List<IBoid> boids;
    IBoid self;

    private void Awake()
    {
        cols = new Collider[maxBoids];
        self = GetComponent<IBoid>();
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
        return Vector3.zero;
    }
}
