using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour, ISteering
{
    public int maxBoids;
    public float radius;
    public LayerMask maskBoids;
    Collider[] _colls;
    List<IBoid> _boids;
    IBoid _self;
    IFlockingBehaviour[] _behaviours;
    private void Awake()
    {
        _colls = new Collider[maxBoids];
        _self = GetComponent<IBoid>();
        _behaviours = GetComponents<IFlockingBehaviour>();
        _boids = new List<IBoid>(maxBoids);
    }
    public Vector3 GetDir()
    {
        _boids.Clear();
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, _colls, maskBoids);
        for (int i = 0; i < count; i++)
        {
            var boid = _colls[i].GetComponent<IBoid>();
            if (boid == null || boid == _self) continue;
            _boids.Add(boid);
        }

        Vector3 dir = Vector3.zero;
        for (int i = 0; i < _behaviours.Length; i++)
        {
            dir += _behaviours[i].GetDir(_boids, _self);
        }

        return dir.normalized;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
