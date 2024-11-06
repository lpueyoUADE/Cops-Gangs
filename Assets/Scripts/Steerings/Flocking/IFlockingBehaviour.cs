using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlockingBehaviour 
{
    Vector3 GetDir(List<IBoid> boids, IBoid self);

    EntityModel Leader { get; }
}
