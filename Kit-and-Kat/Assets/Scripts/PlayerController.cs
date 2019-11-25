using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mPlayerSpeed;
    public Rigidbody2D mPlayer;
    public Rigidbody2D ball;
    public KeyCode mRightKey;
    public KeyCode mLeftKey;

    private KeyCode mThrowKey = KeyCode.Space;
    public static bool IsHoldingBall = false;

//    public static int mScore; 
//    public static int mLife;
//    public static Vector3 defaultPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(mRightKey))
        {
            mPlayer.AddForce(Vector2.right * Time.deltaTime * mPlayerSpeed);
            
        }
        
        else if (Input.GetKey(mLeftKey))
        {
            mPlayer.AddForce(Vector2.left * Time.deltaTime * mPlayerSpeed);
        }

        if (Input.GetKey(mThrowKey))
        {
            if (IsHoldingBall)
            {
                IsHoldingBall = false;
                // throw the ball
            }
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {    
        //Check if the trigger is a ball trigger or cat  
        //Update the game controller that you have the ball?

        IsHoldingBall = true;
        Debug.Log("Got the ball!");
    }
}
