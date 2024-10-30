using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    private Rigidbody rb;
    public Rigidbody Rb { get => rb; set => rb = value; }
    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
}
