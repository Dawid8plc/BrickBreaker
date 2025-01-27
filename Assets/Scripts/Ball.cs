using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float speed = 5f;

    private Rigidbody rigidBody;

    TrailRenderer trail;

    Vector3 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        speed = GetBaseSpeed();
    }

    public float GetBaseSpeed()
    {
        switch (GameState.GetDifficulty())
        {
            default:
            case Difficulty.Easy:
                return 10f;
            case Difficulty.Normal:
                return 15f;
            case Difficulty.Hard:
                return 20f;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void FixedUpdate()
    {
        rigidBody.velocity = rigidBody.velocity.normalized * speed;

        lastVelocity = rigidBody.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Vector3 reflection = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
            rigidBody.velocity = reflection.normalized * speed;
        }
        else if (collision.collider.CompareTag("Paddle"))
        {
            ContactPoint contact = collision.contacts[0];

            float relativeX = transform.position.x - collision.transform.position.x;
            float normalizedRelativeX = relativeX / (collision.collider.bounds.size.x / 2f);

            float bounceAngle = normalizedRelativeX * 45f;

            if (contact.normal.y > 0)
            {
                Vector3 newDirection = Quaternion.Euler(0, 0, bounceAngle) * Vector3.up;
                newDirection.x = -newDirection.x;
                rigidBody.velocity = newDirection.normalized * speed;
            }
            else if (contact.normal.y < 0)
            {
                Vector3 newDirection = Quaternion.Euler(0, 0, -bounceAngle) * Vector3.down;
                newDirection.x = -newDirection.x;
                rigidBody.velocity = newDirection.normalized * speed;
            }
        }
        else if (collision.collider.CompareTag("Block"))
        {
            collision.collider.gameObject.GetComponent<BlockController>().Damage(1);

            Vector3 reflection = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
            rigidBody.velocity = reflection.normalized * speed;
        }
        else if (collision.collider.CompareTag("Border"))
        {
            GameController.GetInstance().BallDestroyed(this);
            Destroy(gameObject);
        }
    }

    internal void EnableTrail()
    {
        trail.enabled = true;
    }
}
