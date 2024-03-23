using UnityEngine;

public class CarController : MonoBehaviour
{
    public SpawnManager spawnManager;

    public float defaultSpeed = 5f; // Adjust this value to set the default speed of the car
    public float accelerationForce = 10f; // Adjust this value to set the acceleration force
    public float brakeForce = 10f; // Adjust this value to set the braking force
    public float turnSpeed = 100f; // Adjust this value to set the turning speed
    public float rotationResetDuration = 0.5f; // Adjust this value to set the duration of rotation reset

    private Rigidbody rb;
    private Quaternion initialRotation;

    private bool accelerating = false;
    private bool braking = false;
    private bool turningLeft = false;
    private bool turningRight = false;

    public float raycastDistance = 5f; // Adjust this value to set the distance of raycast for detecting traffic cars

    private float currentSpeed; // Current speed of the car

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Apply initial constant forward velocity when the game starts
        rb.velocity = transform.forward * defaultSpeed;

        // Record initial rotation
        initialRotation = transform.rotation;

        // Initialize current speed
        currentSpeed = defaultSpeed;
    }

    private void FixedUpdate()
    {
        // Ensure the car continues moving forward at the current speed
        rb.velocity = transform.forward * currentSpeed;

        // Gradually increase speed when accelerating
        if (accelerating)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, defaultSpeed + accelerationForce, Time.deltaTime * accelerationForce);
        }
        // Gradually decrease speed when braking
        else if (braking)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, defaultSpeed, Time.deltaTime * brakeForce);
        }
    }

    private void Update()
    {
        // Rotate the car based on button states
        if (turningLeft)
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (turningRight)
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        
    }

    public void OnAccelerateButtonDown()
    {
        accelerating = true;
    }

    public void OnAccelerateButtonUp()
    {
        accelerating = false;
    }

    public void OnBrakeButtonDown()
    {
        braking = true;
    }

    public void OnBrakeButtonUp()
    {
        braking = false;
    }

    public void OnTurnLeftButtonDown()
    {
        turningLeft = true;
    }

    public void OnTurnLeftButtonUp()
    {
        turningLeft = false;
        // Reset rotation to forward direction
        ResetRotation(Vector3.forward);
    }

    public void OnTurnRightButtonDown()
    {
        turningRight = true;
    }

    public void OnTurnRightButtonUp()
    {
        turningRight = false;
        // Reset rotation to forward direction
        ResetRotation(Vector3.forward);
    }

    // Method to smoothly reset rotation to the specified forward direction
    private void ResetRotation(Vector3 forwardDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < rotationResetDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationResetDuration);
            elapsedTime += Time.deltaTime;
        }

        transform.rotation = targetRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }

    public void OnHornButtonPress()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            TrafficCar trafficCar = hit.collider.GetComponent<TrafficCar>();
            if (trafficCar != null)
            {
                trafficCar.MoveAside();
            }
        }
    }
}
