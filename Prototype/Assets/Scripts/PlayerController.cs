using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody Rb;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }
/// 
    void Update()
    {
        
    }
/// 
    void FixedUpdate()
    {

    }
///
    void OnMove(InputValue movementVaue)
    {
        Vector2 movmentVector = movementVaue.Get<Vector2>();
    }
}
