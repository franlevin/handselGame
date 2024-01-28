using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Running")]
    [SerializeField] float runningSpeed = 10f;
    [SerializeField] float acceleration = 0f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float cancelRate;
    private bool jumpTriggered;
    private bool isJumping = false;

    // Components
    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate para actualizaciones de física
    private void FixedUpdate()
    {
        AutoRun();
        Jump();
        CancelJump();
    }


    // ### CORRER ###
    private void AutoRun()
    {
        rb.velocity = new Vector2(runningSpeed * Time.deltaTime, rb.velocity.y);
    }

    // ### SALTAR ###

    
    private void Jump(){
        if(jumpTriggered && CanJump()){
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJumping = true;
            
        }
    }

    private bool CanJump(){
        bool onGround = rb.IsTouchingLayers(LayerMask.GetMask("Ground"));
        return onGround && !isJumping;
    }

    private void CancelJump(){
        if(!jumpTriggered && CanCancelJump()){
            rb.AddForce(Vector2.down * cancelRate);
        }
    }

    private bool CanCancelJump(){
        bool isMovingUpwards = rb.velocity.y > 0;
        return isMovingUpwards && isJumping;
    }

    // Detección de finalización de salto al tocar el piso
    private void OnCollisionEnter2D(Collision2D other) {
            isJumping = !(other.gameObject.tag == "Ground");
    }

    // Control de presión del botón
    private void OnJump(InputValue input){
        jumpTriggered = input.isPressed;
        
    }



    
}
