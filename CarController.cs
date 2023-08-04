using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 10f;        // Speed of the car's movement
    public float rotationSpeed = 100f;   // Speed of the car's rotation

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        MoveCar();
        RotateCar();
    }

    private void MoveCar()
    {
        // Move the car forward/backward
        Vector3 forwardMovement = transform.forward * verticalInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    private void RotateCar()
    {
        // Rotate the car left/right
        float rotation = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
