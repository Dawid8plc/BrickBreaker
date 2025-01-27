using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    List<Ball> Balls = new List<Ball>();

    [SerializeField] float paddleSpeed = 10f;

    [SerializeField] Transform BallsContainer;

    [SerializeField] GameObject BallPrefab;

    public void AddBall()
    {
        GameObject ball = Instantiate(BallPrefab, BallsContainer);

        Balls.Add(ball.GetComponent<Ball>());
    }

    public float GetPaddleSpeed()
    {
        return paddleSpeed;
    }

    public void SetPaddleSpeed(float paddleSpeed)
    {
        this.paddleSpeed = paddleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.GetInstance().GetGameLost())
            return;

        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontal * paddleSpeed * Time.deltaTime, 0f, 0f));

        if (transform.position.x < -15f)
        {
            transform.position = new Vector3(-15f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 15f)
        {
            transform.position = new Vector3(15f, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var ball in Balls)
            {
                ball.transform.parent = null;

                Rigidbody rb = ball.GetComponent<Rigidbody>();

                rb.AddForce(Vector3.up * ball.GetSpeed(), ForceMode.Impulse);

                Ball ballController = ball.GetComponent<Ball>();
                ballController.EnableTrail();
            }

            Balls.Clear();
        }
    }
}
