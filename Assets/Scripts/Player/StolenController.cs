using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StolenController : MonoBehaviour
{

    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 20;
    public float groundHeight = 0;
    //public bool isGrounded = false;
    private float minDistanceToGroundForJump = 1f;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundThreshold = 1;
    public float playerSpeed = 4f;

    public float playerHeight;
    Vector2 groundControlPos;


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
            if (Input.GetKeyDown(KeyCode.Space))
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
        //transform.position.y - (-1 * (transform.localScale.y / 2))
        groundControlPos = new Vector2(transform.position.x, transform.position.y - 4f);
        Vector2 rayDirection = Vector2.down;

        Debug.DrawRay(groundControlPos, rayDirection, Color.red, 10.0f);

        //Debug.Log("Groundcontrolpos es: " + groundControlPos + "y transform.localScale.y es: " + transform.localScale.y);
    }

    private void FixedUpdate()
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

    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundControlPos, Vector2.down , minDistanceToGroundForJump); //la mindistance deberia cubrir como para iniciar la accion correctamente
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }
}