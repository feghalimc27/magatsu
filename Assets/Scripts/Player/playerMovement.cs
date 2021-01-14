using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    // Vertical Mobility
    [Header("Vertical Mobility")]
    public float fullHopVelocity;
    public float doubleJumpVelocity;
    public float fallSpeed;
    public float gravity;
    public float gravityDescentMultiplier;

    // Horizontal Mobility
    [Header("Horizontal Mobility")]
    public float groundAcceleration;
    public float airAcceleration;
    public float maxRunSpeed;
    public float airSpeed;
    public float friction;
    public float airFriction;

    private Rigidbody2D rb;
    private Vector2 movementVector;

    // Movement Flags
    // DIRECTION: true facing right, false facing left
    private bool direction = true;
    private bool grounded = false;
    private bool hasDJ = true;

    private bool jumpButtonPressed = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        ApplyMovement();
        MovementControls();
        ApplyFriction();
        CutHorizontalSpeed();

        ApplyGravity();
        Jump();
    }

    void MovementControls() {
        if (Mathf.Abs(movementVector.x) < maxRunSpeed && grounded) {
            movementVector.x += groundAcceleration * Input.GetAxis("Horizontal");
        }
        if(Mathf.Abs(movementVector.x) < airSpeed && !grounded) {
            movementVector.x += airAcceleration * Input.GetAxis("Horizontal");
        }
    }

    void Jump() {
        if (Input.GetAxis("Jump") > 0 && !jumpButtonPressed) {
            jumpButtonPressed = true;
            if (grounded) {
                movementVector.y = fullHopVelocity;
                grounded = false;
            }
            else if (hasDJ) {
                movementVector.y = doubleJumpVelocity;
                hasDJ = false;
            }
            else {
                // Do nothing
            }
        }
        
        if (Input.GetAxis("Jump") == 0) {
            jumpButtonPressed = false;
        }
    }

    void ApplyFriction() {
        if (grounded) {
            movementVector.x *= friction;
        }
        else {
            movementVector.x *= airFriction;
        }
    }

    void ApplyGravity() {
        if (movementVector.y > -fallSpeed && movementVector.y > 0 && !grounded) {
            movementVector.y -= gravity;
        }
        else if (movementVector.y > -fallSpeed && movementVector.y <= 0 && !grounded) {
            movementVector.y -= gravity * gravityDescentMultiplier;
        }

        if (movementVector.y < -fallSpeed && !grounded) {
            movementVector.y = -fallSpeed;
        }
    }

    void CutHorizontalSpeed() {
        if (Mathf.Abs(rb.velocity.x) < 0.05 && Input.GetAxis("Horizontal") == 0) {
            rb.velocity = new Vector2(0, movementVector.y);
        }
    }

    void ApplyMovement() {
        rb.velocity = movementVector;
    }

    public bool GetDirection() {
        return direction;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = true;
            hasDJ = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = false;
        }
    }
}
