using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloatController : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] Vector3 direction;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update

    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPosition + (direction * (Mathf.Sin(Time.time * frequency) + 1) * amplitude);
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
