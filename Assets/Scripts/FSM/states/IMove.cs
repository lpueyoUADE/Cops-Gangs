using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void Move(Vector3 direction);
    void MoveSlow(Vector3 direction);
    void Look(Vector3 direction);
    void Look(Transform target);

    public void SetPosition(Vector3 position);
}