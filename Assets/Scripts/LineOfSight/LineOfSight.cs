using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform pov;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacle;
    //[SerializeField] private float delayToLooseTarget;

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
    Vector3 Origin { 
        get { 
            if (pov == null) { return transform.position; }
            else return pov.position;

            } 
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
