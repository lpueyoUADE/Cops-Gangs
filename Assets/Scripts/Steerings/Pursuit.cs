using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : ISteering
{
    Transform _entity;
    Rigidbody _target;
    float _timePrediction;

    public Pursuit(Transform entity, Rigidbody target, float timePrediction)
    {
        _entity = entity;
        _target = target;
        _timePrediction = timePrediction;
    }

    public Vector3 GetDir()
    {
        Vector3 point = _target.position + _target.transform.forward * _target.velocity.magnitude * _timePrediction;
        Vector3 dirToPoint = (point - _entity.position).normalized;
        Vector3 dirToTarget = (_target.position - _entity.position).normalized;
        if (Vector3.Dot(dirToPoint, dirToTarget) < 0)
        {
            return dirToTarget;
        }
        else
        {
            return dirToPoint;
        }
    }
    
}
