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
    public float jumpHeight;
    float initialHeight;
	float laneTransitionTimer;
	float LANE_TRANSITION_TIME;
    
    public float posOnWheel = 30f;      // the player's position on the wheel, for use in obstacle hit
                                        // uses degrees as unit


    public float animationTimer;               // The timer for the player animation
    public int playerFrame;                    // The player's current frame
	protected int stage;
    public Texture2D[] playerFrames;           // Player's textures
    public Texture2D[] playerFrames2;
    public Texture2D[] playerFrames3;
	protected Texture2D[] pFrames;
    public GameObject playerImage;             // Player's current "texture"
    public GameManagerInit gameManager;
    public bool beenHit;                       // True is player is currently in "being hit" animation
    float hitTimer;
    public float playerHealth;
    private const float playerMaxHealth = 30;
    public AudioClip hitSound;
    public AudioClip jumpSound;
    public AudioSource source;
    public float previousHitPitch;
    public float previousJumpPitch;
    

    public static float PlayerMaxHealth
    {
        get
        {
            return playerMaxHealth;
        }
    }


    // Use this for initialization
    void Start () {
		stage = 0;
        currentLane = Location.lane.middle; // start in the middle lane                                            
        timer = 0; // reset time
		laneTransitionTimer = 0;
		LANE_TRANSITION_TIME = 0.1f;
        playerPosition = 0; // lane 0 is the center lane
        curState = playerState.inLane;
		laneWidthInt = (int)laneWidth;
        playerFrame = 0;
        animationTimer = 0;
        jumpHeight = 18f;
        initialHeight = gameObject.transform.position.y;
        playerImage = GameObject.FindGameObjectWithTag("PlayerFrame");
        beenHit = false;
        playerHealth = PlayerMaxHealth;
        source = GetComponent<AudioSource>();
        previousHitPitch = 1.4f;
        previousJumpPitch = 1.0f;
		pFrames = playerFrames;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && curState == playerState.inLane) MoveLeft();         // move left                                                                                            
        else if (Input.GetKeyDown(KeyCode.RightArrow) && curState == playerState.inLane) MoveRight();  // move right
        else if (Input.GetKeyDown(KeyCode.Space) && curState == playerState.inLane)                    // jump
        {
            curState = playerState.jumping;
            initialHeight = gameObject.transform.position.y;
            float nextJumpPitch = Random.Range(1.0f, 1.4f);
            while (nextJumpPitch < previousJumpPitch + 0.05f && nextJumpPitch > previousJumpPitch - 0.05f)
            {
                nextJumpPitch = Random.Range(0.8f, 1.2f);
            }
            source.pitch = nextJumpPitch;
            source.PlayOneShot(jumpSound, Random.Range(0.1f, 1.0f));
            previousJumpPitch = nextJumpPitch;

            source.PlayOneShot(jumpSound);
        }


        //Handle playert hit animation
        if (beenHit)
        {
            hitTimer += Time.deltaTime;
            timer += Time.deltaTime;
            //Handles player bounceback
            if (hitTimer <= 0.5)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialHeight + (3 * Mathf.Sin((timer * Mathf.PI) / 0.5f)), gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.y != initialHeight)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialHeight, gameObject.transform.position.z);
            }
            //Handles player blinking
            if (hitTimer % 0.25 < 0.125)
            {
                playerImage.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                playerImage.GetComponent<Renderer>().enabled = true;
            }
            //Ends hit animation
            if (hitTimer > 1.5)
            {
                hitTimer = 0;
                beenHit = false;
                playerImage.GetComponent<Renderer>().enabled = true;
            }
        }

        // Handle jumping animation
        if (curState == playerState.jumping)
        {
            timer += Time.deltaTime;
            if (timer <= 0.5)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialHeight + (jumpHeight * Mathf.Sin((timer * Mathf.PI) / 0.5f)), gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.y != initialHeight)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialHeight, gameObject.transform.position.z);
                timer = 0;
                curState = playerState.inLane;
            }
            else
            {
                timer = 0;
                curState = playerState.inLane;
            }
          
        }

        // transition the model left
        if (curState == playerState.movingLeft)
        {
			// increment timer
			timer += Time.deltaTime;

			// increment timer
			timer += Time.deltaTime;
			// increment lane timer
			laneTransitionTimer += Time.deltaTime;

			float offset = 0f;
			if (currentLane == Location.lane.middle) offset = +9f;
			transform.position = new Vector3((-9f)*(laneTransitionTimer/LANE_TRANSITION_TIME) + offset,transform.position.y,transform.position.z);
			if (laneTransitionTimer >= LANE_TRANSITION_TIME) {
				transform.position = new Vector3(-9f + offset,transform.position.y,transform.position.z);
				curState = playerState.inLane;
				laneTransitionTimer = 0;
			}

			/*// if the player is not far enough to the left, move them left
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
            }*/
        }

        // transition the model right
        if (curState == playerState.movingRight)
        {
			
            // increment timer
            timer += Time.deltaTime;
			// increment lane timer
			laneTransitionTimer += Time.deltaTime;

			float offset = 0f;
			if (currentLane == Location.lane.middle) offset = -9f;
			transform.position = new Vector3((9f)*(laneTransitionTimer/LANE_TRANSITION_TIME) + offset,transform.position.y,transform.position.z);
			if (laneTransitionTimer >= LANE_TRANSITION_TIME) {
				transform.position = new Vector3(9f + offset,transform.position.y,transform.position.z);
				curState = playerState.inLane;
				laneTransitionTimer = 0;
			}

			/*// if the player is not far enough to the right, move them right
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
            }*/
        }
        

        //This section handles the 2D Animation
        animationTimer += Time.deltaTime;
        if (animationTimer >= 0.03)
        {
			if(gameManager.currentLifeStage != stage) {
				stage = gameManager.currentLifeStage;
				switch(stage) {
				case 0:
					pFrames = playerFrames;
					transform.localScale = new Vector3(12,5,5);
					break;
				case 1:
					pFrames = playerFrames2;
					transform.localScale = new Vector3(12,7,5);
					initialHeight = 8;
					break;
				case 2:
					pFrames = playerFrames3;
					transform.localScale = new Vector3(12,10,5);
					initialHeight = 11;
					break;
				}
			}
            // Uses the second from of animation if player is currently jumping
            if (curState == playerState.jumping)
            {
				playerImage.GetComponent<Renderer>().material.SetTexture("_MainTex", pFrames[1]);
            }
            else
            {
                // Picks the current frame of animation based on the timer
                animationTimer = 0;
                playerFrame += 1;
                if (playerFrame > 13) playerFrame = 0;
                /*switch (gameManager.currentLifeStage)
                {
                    case 0:
                        playerImage.GetComponent<Renderer>().material.SetTexture("_MainTex", playerFrames[playerFrame]);
                        break;
                    case 1:
                        playerImage.GetComponent<Renderer>().material.SetTexture("_MainTex", playerFrames2[playerFrame]);
                        break;
                    case 2:
                        playerImage.GetComponent<Renderer>().material.SetTexture("_MainTex", playerFrames3[playerFrame]);
                        break;
                    default:
                        break;
                }*/
                playerImage.GetComponent<Renderer>().material.SetTexture("_MainTex", pFrames[playerFrame]);
				
            }
        }
        
    }

    //Called be GameManagerInit when player is hit
    public void HitPlayer()
    {
        beenHit = true;
        float nextHitPitch = Random.Range(1.4f, 1.8f);
        while (nextHitPitch < previousHitPitch + 0.05f && nextHitPitch > previousHitPitch - 0.05f)
        {
            nextHitPitch = Random.Range(1.4f, 1.8f);
        }
        source.pitch = nextHitPitch;
        source.PlayOneShot(hitSound, Random.Range(0.1f, 1.0f));
        previousHitPitch = nextHitPitch;
    }

	// moves one lane to the right based on what lane the player is in
    public void MoveRight()
    {
        switch  (currentLane)
        {
            case Location.lane.left:
                currentLane = Location.lane.middle;
                curState = playerState.movingRight;
				laneTransitionTimer = 0;
                break;
            case Location.lane.middle:
                currentLane = Location.lane.right;
                curState = playerState.movingRight;
				laneTransitionTimer = 0;
                break;
            case Location.lane.right:
                break;
            default:
                break;
        }
    }

	// moves one lane to the left based on what lane the player is in
    public void MoveLeft()
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

	public void Jump() {
		// code here 
	}

	public void DisplayWord() {

	}
}
