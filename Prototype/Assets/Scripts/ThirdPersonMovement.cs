using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour {
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    //Turning/looking:
    public float turnSmoothTime = 0.1f;
    float tunrSmoothVelocity;

    void Update() {
        //Player WASD movement:
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
        //Jumping
        //if (Input.GetKeyDown(KeyCode.Space)) {
            //transform.position = transform.position + new Vector3(0f, jumpHeight, 0f).normalized;
        //}
        
        ////
        if (Input.GetAxisRaw("Jump")) {
                Debug.Log("Input inputs");
                velocity = Mathf.Sqrt(jumpHeight * -2f * (gravityScale));
            }
            velocity += gravity * gravityScale * Time.deltaTime;
            MovePlayer();
    }


        public CharacterController cc;
        public float gravity = -9.81f;
        public float gravityScale = 1;
        public float jumpHeight = 4;
        float velocity;
        
        void MovePlayer() {
            cc.Move(new Vector3(0, velocity, 0) * Time.deltaTime);
        }
    

/*public class CharacterControllerJump : MonoBehaviour {
    public CharacterController cc;
    public float gravity = -9.81f;
    public float gravityScale = 1;
    public float jumpHeight = 4;
    float velocity;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Input inputs");
            velocity = Mathf.Sqrt(jumpHeight * -2f * (gravityScale));
        }
        velocity += gravity * gravityScale * Time.deltaTime;
        MovePlayer();
    }
    void MovePlayer() {
        cc.Move(new Vector3(0, velocity, 0) * Time.deltaTime);
    }
}*/
}

        
