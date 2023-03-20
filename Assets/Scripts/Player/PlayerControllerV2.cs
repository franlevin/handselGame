using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    private Dictionary<string, bool> powerUps = new Dictionary<string, bool>();
    /* For reference> Dictionaries are not serializable in Unity. 
        * This means that even public Dictionaries will not appear in the Inspector 
        * and will not save between play sessions. 
        access through: powerUps["ability"]      
        */

    //ALL initial values need to be adjusted

    public float baseSpeed = 2f;
    public float dashSpeed = 6f;

    //For jump
    public float baseJumpHeight = 10f;
    private float minDistanceToGroundForJump = 1f;
    private float jumpCharge = 0f;
    public float maxJumpCharge = 200f;
    private float jumpPower = 20f;

    //For dash
    private float dashCounter = 0f;
    private float dashDuration = 100f;

    //Input
    public KeyCode jumpKey = KeyCode.J;
    public KeyCode dashKey = KeyCode.D;



    void Start()
    {
        //initiate player powerups status
        powerUps.Add("doubleJump", false);
        powerUps.Add("dash", false);
    }


    void FixedUpdate()
    {
        DashControl();
        JumpControl();

        moveForward(baseSpeed);

        doDebug();
    }

    ///Methods

    //Basic forward movement
    private void moveForward(float speed)
    {
        if (jumpCharge != 0)
        {
            if (Input.GetKeyDown(jumpKey)) //if is charging a jump, player must not move forward
            {
                speed = 0;
            }
            else//if jumping, advance slower?
            {
                speed = 1;
            }
        }
        else if (dashCounter > 0)
        {
            speed = dashSpeed;
            dashCounter--;
        }

        transform.position += Vector3.right * Time.deltaTime * speed;
    }

    //Basic Jump
    private void JumpControl()
    {
        if (IsGrounded())
        {
            if (Input.GetKeyDown(jumpKey))  //inicia carga de salto, resetea a 0
            {
                jumpCharge = 0;
                dashCounter = 0;      //por las dudas para que no dashee automaticamente despues de aterrizar un salto
            }
            else if (Input.GetKey(jumpKey)) //carga salto por cada frame presionando
            {
                jumpCharge++;
            }
            else if (Input.GetKeyUp(jumpKey))    //al soltar inicia accion de salto
            {
                if (jumpCharge > maxJumpCharge)
                {
                    jumpCharge = maxJumpCharge; //limita la potencia del salto a la max carga permitida
                }
                SimpleJump();                   //salta
            }
        }

        else //If not Grounded
        {
            if (jumpCharge > 0)
            {
                SimpleJump();                       //si no esta en el suelo, y tiene carga de salto, salta
                jumpCharge--;
            }
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, minDistanceToGroundForJump); //la mindistance deberia cubrir como para iniciar la accion correctamente
    }
    private void SimpleJump()
    {
        Vector2 pos = transform.position;
        pos.y += jumpPower * Time.fixedDeltaTime;
        transform.position = pos;
    }


    //Add & remove power ups
    private void gainPowerUp(string powerUp) { powerUps[powerUp] = true; }
    private void losePowerUp(string powerUp) { powerUps[powerUp] = false; }


    //Control powerups
    private void DashControl()
    {
        if (Input.GetKeyDown(dashKey)) //nada previene spammear dashes ATM
        {
            dashCounter = dashDuration;
        }
    }

    private void DoubleJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            if (!IsGrounded())
            {

            }
        }

    }

    //Just to show logs during prod and not have it mid-code
    void doDebug()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            powerUps["dash"] = !powerUps["dash"];
        }

        Debug.Log("jcs" + jumpCharge); Debug.Log("jp" + jumpPower);

    }
}

