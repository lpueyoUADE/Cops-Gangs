using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public float rotationSpeed = 60f; // Speed of rotation in degrees per second
    public float movementRange = 0.5f; // Range of movement up and down
    public float movementSpeed = 1f; // Speed of movement in units per second
    [SerializeField] UnityEvent OnCollection;

    private Vector3 initialPosition;

    private void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Rotate the object around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Calculate the vertical movement offset using a sine wave
        float yOffset = Mathf.Sin(Time.time * movementSpeed) * movementRange;

        // Apply the vertical movement offset
        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollection.Invoke();
        }                
    }
}