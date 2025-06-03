using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : ISteering
{
    Transform _entity;
    Rigidbody _target;
    float _timePrediction;

    public Evade(Transform entity, Rigidbody target, float timePrediction)
    {
        _entity = entity;
        _target = target;
        _timePrediction = timePrediction;
    }

    public Vector3 GetDir()
    {
        Vector3 point = _target.position + _target.transform.forward * _target.velocity.magnitude * _timePrediction;
        Vector3 dirEvade = (_entity.position - point).normalized;
        Vector3 dirFlee = (_entity.position - _target.position).normalized;
        if (Vector3.Dot(dirEvade, dirFlee) < 0)
        {
            return dirFlee;
        }
        else
        {
            return dirEvade;
        }
    }
    public float TimePrediction
    {
        set
        {
            _timePrediction = value;
        }
    }
}
