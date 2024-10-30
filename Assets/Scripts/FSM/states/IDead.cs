using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDead
{
    void Die();
    bool IsDead { get; set; }
}
