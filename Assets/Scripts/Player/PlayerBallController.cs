using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBallController : MonoBehaviour
{
    private Rigidbody _rb;
    private Keyboard keyboard;
    private Vector3 startPos;

    public float acceleration = 10f; // force applied while moving
    public float maxSpeed = 5f;      // max velocity cap
    public float deceleration = 5f;  // rate at which velocity drops when no input

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        keyboard = Keyboard.current;
        startPos = transform.position;
    }

    void FixedUpdate()
    {

        if (keyboard == null)
        {
            Debug.LogWarning("Keyboard not detected.");
            return;
        }

        Vector3 inputDir = Vector3.zero;

        if (keyboard.wKey.isPressed) inputDir += Vector3.forward;
        if (keyboard.sKey.isPressed) inputDir += Vector3.back;
        if (keyboard.aKey.isPressed) inputDir += Vector3.left;
        if (keyboard.dKey.isPressed) inputDir += Vector3.right;

        if (inputDir != Vector3.zero)
        {
            // Normalize input direction to avoid faster diagonal movement
            inputDir.Normalize();

            // Only apply force if under max speed
            Vector3 horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            if (horizontalVelocity.magnitude < maxSpeed)
            {
                _rb.AddForce(inputDir * acceleration, ForceMode.Acceleration);
            }

            // Cap max velocity
            //if (_rb.linearVelocity.magnitude > maxSpeed)
            //{
            //    _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
            //}
        }
        //else
        //{
        //    // Deceleration when no input
        //    _rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        //}

        // Reset position if falling off
        if (transform.position.y < -15)
        {
            transform.position = startPos;
            _rb.linearVelocity = Vector3.zero; // stop momentum
        }
    }
}
