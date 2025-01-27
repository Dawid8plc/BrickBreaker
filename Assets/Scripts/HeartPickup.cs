using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);

        rigidbody.MovePosition(transform.position + new Vector3(0f, -8f, 0f) * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paddle")
        {
            int lives = GameState.GetLives();

            if(lives < 99)
                GameState.SetLives(lives + 1);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
