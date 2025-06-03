using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider bc;
    public Rigidbody Rb { get => rb; set => rb = value; }
    public BoxCollider Bc { get => bc; set => bc = value; }

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Bc = GetComponent<BoxCollider>();
    }
}
