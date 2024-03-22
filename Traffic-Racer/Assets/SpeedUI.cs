using UnityEngine;
using TMPro;

public class SpeedUI : MonoBehaviour
{
    public Rigidbody carRigidbody; // Reference to the car's Rigidbody component
    public TextMeshProUGUI speedText; // Reference to the TextMeshProUGUI component to display speed

    private void Update()
    {
        // Ensure carRigidbody is not null and speedText is assigned
        if (carRigidbody != null && speedText != null)
        {
            // Calculate car's speed magnitude
            float speed = carRigidbody.velocity.magnitude * 3.6f; // Multiply by 3.6 to convert from m/s to km/h

            // Update the text to display the speed
            speedText.text = "Speed: " + Mathf.Round(speed) + " km/h";
        }
    }
}
