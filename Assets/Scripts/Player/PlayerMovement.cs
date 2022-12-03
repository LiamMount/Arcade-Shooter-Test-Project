using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Technical stuff
    GameManager gm;

    //Base stuff
    Rigidbody2D rb;

    public float moveSpeed = 1f;

    //Move values
    const string LEFT = "left";
    const string RIGHT = "right";
    const string UP = "up";
    const string DOWN = "down";

    string horizontalButtonPressed;
    string verticalButtonPressed;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!gm.isPaused && !gm.isCutscene)
        {
            GetMovementValues();
            DoMovement();
        }
        if (gm.isCutscene)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void GetMovementValues()
    {
        //Horizontal movement
        //Right Controller
        if (Input.GetAxisRaw("HorizontalStick") > 0.1)
        {
            horizontalButtonPressed = RIGHT;
        }
        //Left Controller
        else if (Input.GetAxisRaw("HorizontalStick") < -0.1)
        {
            horizontalButtonPressed = LEFT;
        }

        //Right
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalButtonPressed = RIGHT;
        }
        //Left
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            horizontalButtonPressed = LEFT;
        }
        /*
        //Stop if both are held
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            horizontalButtonPressed = null;
        }
        */
        //Stop
        else
        {
            horizontalButtonPressed = null;
        }

        //Vertical movement
        //Up Controller
        if (Input.GetAxisRaw("VerticalStick") > 0.1)
        {
            horizontalButtonPressed = UP;
        }
        //Down Controller
        else if (Input.GetAxisRaw("VerticalStick") < -0.1)
        {
            horizontalButtonPressed = DOWN;
        }

        //Up
        else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            verticalButtonPressed = UP;
        }
        //Down
        else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            verticalButtonPressed = DOWN;
        }
        /*
        //Stop if both are held
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            verticalButtonPressed = null;
        }
        */
        //Stop
        else
        {
            verticalButtonPressed = null;
        }
    }

    public void DoMovement()
    {
        //Horizontal
        //Right
        if (horizontalButtonPressed == RIGHT)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        //Left
        else if (horizontalButtonPressed == LEFT)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        //Stop
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Vertical
        //Up
        if (verticalButtonPressed == UP)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
        //Down
        else if (verticalButtonPressed == DOWN)
        {
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
        }
        //Stop
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
