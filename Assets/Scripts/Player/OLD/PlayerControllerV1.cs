using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float dashForce = 20.0f;
    public float dashDuration = 0.5f;

    private Rigidbody2D rb2D;
    private float dashTime;
    private float dashStart;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Dash"))
        {
            dashStart = Time.time;
            dashTime = dashDuration;
        }

        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            rb2D.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
    }
}