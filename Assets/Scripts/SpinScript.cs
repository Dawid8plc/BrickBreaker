using UnityEngine;

public class SpinScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
