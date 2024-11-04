using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform pov;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacle;

    Collider[] entitiesInRange;
    public Collider[] EntitiesInRange { get => entitiesInRange; }

    private void Start()
    {
        entitiesInRange = new Collider[10];
    }
    public bool CheckRange(Transform target)
    {
        float distanceToTarget = Vector3.Distance(target.position, Origin);
        return distanceToTarget <= range;
    }

    public bool CheckAngle(Transform target)
    {
        Vector3 dirToTarget = target.position - Origin;
        float angleToTarget = Vector3.Angle(dirToTarget, Foward);
        return angleToTarget <= angle/2;
    }

    public bool InView(Transform target)
    {
        Vector3 dirToTarget = target.position - Origin;
        return !Physics.Raycast(Origin, dirToTarget.normalized, dirToTarget.magnitude, obstacle);
    }

    public bool InSight(Transform target)
    {
        return InView(target) && CheckRange(target) && CheckAngle(target);
    }

    Vector3 Origin { 
        get 
        {
            if (pov == null) { return transform.position; }
            else return pov.position;
        } 
    }

    public int GetEntitiesInRange(Vector3 position, LayerMask mask)
    {
        return Physics.OverlapSphereNonAlloc(position, range, EntitiesInRange, 1 << mask.value);
    }

    Vector3 Foward
    {
        get
        {
            if (pov == null) { return transform.forward; }
            else return pov.forward;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Origin, range);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(Origin, Quaternion.Euler(0,angle/2,0) * Foward * range);
        Gizmos.DrawRay(Origin, Quaternion.Euler(0, -angle / 2, 0) * Foward * range);
    }
}
