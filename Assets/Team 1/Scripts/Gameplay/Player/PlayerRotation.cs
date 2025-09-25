using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private float _rotationSpeed;


    public void Initialize(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }


    public void Rotate(Vector2 lookInput, Vector2 moveInput)
    {
        // Rotate with look input (if using mouse/gamepad look)
        if (lookInput.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(lookInput.x, lookInput.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        // Or rotate to face movement direction
        else if (moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}