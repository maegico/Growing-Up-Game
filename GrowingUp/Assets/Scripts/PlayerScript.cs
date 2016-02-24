﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// player exists in only one of these states
    enum playerState
    {
        inLane,
        movingLeft,
        movingRight,
        jumping
    }

    playerState curState;               // the player's current state
    public Location.lane currentLane;   // the player's lane
    float timer;                        // timer used for transitioning between lanes
    int playerPosition;                 // the player model's world-position represented by an integer
    public float laneWidth = 9f;        // the width of a lane in word-coordinates
    protected int laneWidthInt;         // the width of a lane rounded to an integer

    
    public float posOnWheel = 30f;      // the player's position on the wheel, for use in obstacle hit
                                        // uses degrees as unit


    // Use this for initialization
    void Start () {
		
        currentLane = Location.lane.middle; // start in the middle lane                                            
        timer = 0; // reset time
        playerPosition = 0; // lane 0 is the center lane
        curState = playerState.inLane;
		laneWidthInt = (int)laneWidth;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && curState == playerState.inLane) MoveLeft();         // move left                                                                                            
        else if (Input.GetKeyDown(KeyCode.RightArrow) && curState == playerState.inLane) MoveRight();  // move right

        // transition the model left
        if (curState == playerState.movingLeft)
        {
			// increment timer
			timer += Time.deltaTime;
			// if the player is not far enough to the left, move them left
            if (gameObject.transform.position.x > (-9 + playerPosition))
            {
                gameObject.transform.Translate(new Vector3(-Time.deltaTime * (9f / 0.1f), 0, 0));
            }
			// if the game object is too far left, move them back
            if (gameObject.transform.position.x <= (-9 + playerPosition))
            {
                timer = 0;
                curState = playerState.inLane;
                playerPosition -= 9;
                gameObject.transform.position = new Vector3((float)playerPosition, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }

        // transition the model right
        if (curState == playerState.movingRight)
        {
            // increment timer
            timer += Time.deltaTime;
			// if the player is not far enough to the right, move them right
            if (gameObject.transform.position.x < (9 + playerPosition))
            {
                gameObject.transform.Translate(new Vector3(Time.deltaTime * (9f / 0.1f), 0, 0));
            }
			// if the game object is too far right, move them back
            if (gameObject.transform.position.x >= (9 + playerPosition))
            {
                timer = 0;
                curState = playerState.inLane;
                playerPosition += 9;
                gameObject.transform.position = new Vector3((float)playerPosition, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }

	// moves one lane to the right based on what lane the player is in
    void MoveRight()
    {
        switch  (currentLane)
        {
            case Location.lane.left:
                currentLane = Location.lane.middle;
                curState = playerState.movingRight;
                break;
            case Location.lane.middle:
                currentLane = Location.lane.right;
                curState = playerState.movingRight;
                break;
            case Location.lane.right:
                break;
            default:
                break;
        }
    }

	// moves one lane to the left based on what lane the player is in
    void MoveLeft()
    {
        switch (currentLane)
        {
            case Location.lane.left:
                break;
            case Location.lane.middle:
                currentLane = Location.lane.left;
                curState = playerState.movingLeft;
                break;
            case Location.lane.right:
                currentLane = Location.lane.middle;
                curState = playerState.movingLeft;
                break;
            default:
                break;
        }

    }
}
