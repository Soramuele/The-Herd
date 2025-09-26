using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    bool isFalling = false;
    float downSpeed = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "sheep")
        {
            // Start falling, delete trap and sheep after 10 seconds
            isFalling = true;
            Destroy(gameObject, 10f);
            Destroy(collision.gameObject, 10f);
        }
    }

    void Update()
    {
        if (isFalling)
        {
            // Increase falling speed over time and move down
            downSpeed += Time.deltaTime/10;
            transform.position -= new Vector3(0, downSpeed, 0);
        }
    }
}
