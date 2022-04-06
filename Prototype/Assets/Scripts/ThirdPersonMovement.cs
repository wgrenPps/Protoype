using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour {
    //moving junk
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;

    //Jumping junk
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    
    //Rotating/looking:
    private float turnSmoothTime = 0.1f;
    float tunrSmoothVelocity;

    void Update() {
        //WASD movement:
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if(direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref tunrSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        //Jumping movement: 
        groundedPlayer = controller.isGrounded;

        if (Input.GetButtonDown("Jump") && groundedPlayer == true) {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}