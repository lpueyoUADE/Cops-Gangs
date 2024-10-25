using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void Move(Vector3 direction);

    void Look(Vector3 direction);

    void Look(Transform target);

    void SetPosition(Vector3 position);
}
