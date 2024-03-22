using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform target; // Reference to the car's transform

    private Vector3 offset; // Offset from the car to the camera

    private void Start()
    {
        // Calculate the initial offset
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        // Ensure the target (car) is valid
        if (target == null)
            return;

        // Set the camera's position to the target's position plus the offset
        transform.position = target.position + offset;

        // Make the camera look at the target
        transform.LookAt(target);
    }
}
