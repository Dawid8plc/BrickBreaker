using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    [Header("Hover Settings")]
    [SerializeField] float hoverHeight = 0.5f; // Maximum height offset for hovering
    [SerializeField] float hoverSpeed = 2.0f;  // Speed of the hovering motion

    private Vector3 startPosition;

    void Start()
    {
        // Record the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new vertical offset using a sine wave
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Apply the offset to the object's position
        transform.position = startPosition + new Vector3(0, hoverOffset, 0);
    }
}
