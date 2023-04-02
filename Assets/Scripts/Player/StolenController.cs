using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class StolenController : MonoBehaviour
{
    //Gravity
    public float gravity;
    public Vector2 velocity;

    //Jumping (Need to clean)
    public float jumpVelocity = 20;
    public float groundHeight = 0;
    private float minDistanceToGroundForJump = 1f;
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;
    public float jumpGroundThreshold = 1;

    //Speed
    private float playerSpeed = 50f;
    public float normalSpeed = 50f;
    public float dashSpeed = 100f;

    //Grounded control
    public float playerHeight;
    Vector2 groundControlPos;

    //For dash
    private float dashTimer = 0.0f;
    private float dashMaxDuration = 0.4f;
    private bool isDashing = false;

    //Input
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.D; 

    void Start()
    {
        playerHeight = transform.position.y;
        //groundControlPos = new Vector2(transform.position.x, transform.position.y / 2);
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        //Debug.Log("Está grounded? Rta: " + IsGrounded());

        if (IsGrounded() || groundDistance <= jumpGroundThreshold)
        {
            //Debug.Log("Está grounded y ground distance menor a threshold");
            if (Input.GetKeyDown(jumpKey))
            {
                //isGrounded = false;
                Debug.Log("Space pressed");
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        //Dash
        if (Input.GetKeyDown(dashKey)) 
        {
            isDashing = true;
            dashTimer = 0;
            playerSpeed = dashSpeed;
        }

        //transform.position.y - (-1 * (transform.localScale.y / 2))
        groundControlPos = new Vector2(transform.position.x, transform.position.y - 4f);
        Vector2 rayDirection = Vector2.down;

        Debug.DrawRay(groundControlPos, rayDirection, Color.red, 10.0f);

        //Debug.Log("Groundcontrolpos es: " + groundControlPos + "y transform.localScale.y es: " + transform.localScale.y);
    }

    private void FixedUpdate()
    {
        JumpControl();
        DashControl();

    }

    private void JumpControl()
    {
        Vector2 pos = transform.position;
        pos += Vector2.right * Time.deltaTime * playerSpeed;

        if (isHoldingJump || !IsGrounded())        //(!IsGrounded())
        {

            holdJumpTimer += Time.fixedDeltaTime;
            if (holdJumpTimer >= maxHoldJumpTime)
            {
                isHoldingJump = false;
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            /*if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                //isGrounded = true;
            }*/
        }

        transform.position = pos;
    }

    private void DashControl()
    {
        if (isDashing && dashTimer < dashMaxDuration) { dashTimer += Time.fixedDeltaTime; }
        else { isDashing = false; dashTimer = 0; playerSpeed = normalSpeed; }
    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Eye")
        {
            
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundControlPos, Vector2.down , minDistanceToGroundForJump); //la mindistance deberia cubrir como para iniciar la accion correctamente
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }
}