using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mPlayerSpeed;
//    public Rigidbody2D mPlayer;
    public Transform mPlayer;
    public Rigidbody2D ball;
    public KeyCode mRightKey;
    public KeyCode mLeftKey;
    public KeyCode mUpKey;
    public float jumpForce;

    private KeyCode mThrowKey = KeyCode.Space;
//    private float mPlayerAngle = 0f;
    private bool mCanJump = false;
    public static bool IsHoldingBall = false;
    
    
    readonly float gravity = -9.1f;
    private float ground_Y = -2.74f; //should be 0 in the real game
    private bool isGrounded = true;
    private float velocity_Y;
//    private float deltaFromLastColl = 0f;
    private Quaternion NormRotation;

    // Start is called before the first frame update
    void Start()
    {
        NormRotation = mPlayer.rotation;
    }

    // Update is called once per frame
    void Update()
    {
//        float xValue = 0;
        if (Input.GetKey(mRightKey))
        {
            mPlayer.transform.Translate(Vector2.right * (mPlayerSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(mLeftKey))
        {
            mPlayer.transform.Translate(Vector2.left * (mPlayerSpeed * Time.deltaTime));
        }

        if (Input.GetKey(mUpKey) && isGrounded)
        {
            velocity_Y = jumpForce;
            isGrounded = false;
        }

        // If the player still didnt touched a ground continue using the gravity force on it.
        if (!isGrounded)
        {
            float newPosition_Y = mPlayer.position.y + velocity_Y * Time.deltaTime;
            if (newPosition_Y <= ground_Y)
            {
                mPlayer.position = new Vector2(mPlayer.position.x, ground_Y);
                isGrounded = true;
                velocity_Y = 0;
            }
            else
            {
               mPlayer.position = new Vector2(mPlayer.position.x, newPosition_Y);
                velocity_Y += gravity * Time.deltaTime;
            }
        }
        
        
        
        // Throwing the ball +++++++++++++ need to be executed
        if (Input.GetKey(mThrowKey))
        {
            if (IsHoldingBall)
            {
                IsHoldingBall = false;
                // throw the ball
            }
        }
    }

    // When colliding with obstacles calculates the new angle of the motion
    public void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
        velocity_Y = 0; // do i need this?
        mPlayer.rotation = other.transform.rotation;
        Debug.Log("Touched an obstacle!! " + mPlayer.rotation.z);
    }
    
    // When stop colliding with obstacles, returns to it's original angle
    public void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Fell from an obstacle!!");
        isGrounded = false;
        mPlayer.rotation = NormRotation;
    }

    // +++++++++++++ need to be executed
    public void OnTriggerEnter2D(Collider2D other) 
    {    

        //Check if the trigger is a ball trigger or cat  
        //Update the game controller that you have the ball? 
        IsHoldingBall = true;
        Debug.Log("Got the ball!");
    }
}
