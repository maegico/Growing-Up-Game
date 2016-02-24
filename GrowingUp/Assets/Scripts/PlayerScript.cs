using UnityEngine;
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

	// the player's current state
    playerState curState;
	// the player's lane
    public Location.lane currentLane;
	// timer used for transitioning between lanes
    float timer;
	// the player model's world-position represented by an integer
    int playerPosition;
	// the width of a lane in word-coordinates
	public float laneWidth = 9f;
	// the width of a lane rounded to an integer
	protected int laneWidthInt;

	// the player's position on the wheel, for use in obstacle hit
	// uses degrees as unit
	public float posOnWheel = 30f; 
    
    
	// Use this for initialization
	void Start () {
		// start in the middle lane
        currentLane = Location.lane.middle;
        // reset time
		timer = 0;
        // lane 0 is the center lane
		playerPosition = 0;
        curState = playerState.inLane;

		laneWidthInt = (int)laneWidth;
	}
	
	// Update is called once per frame
	void Update () {
		// move left
        if (Input.GetKeyDown(KeyCode.LeftArrow) && curState == playerState.inLane) MoveLeft();
		// move right
        if (Input.GetKeyDown(KeyCode.RightArrow) && curState == playerState.inLane) MoveRight();

		// transition the model left
        if (curState == playerState.movingLeft)
        {
			// increment timer
			timer += Time.deltaTime;
			// if the player is not far enough to the left, move them left
            if (gameObject.transform.position.x >= (-9 + playerPosition))
            {
                gameObject.transform.Translate(new Vector3(-Time.deltaTime * (9f / 0.1f), 0, 0));
            }
			// if the game object is too far left, move them back
            else
            {
                timer = 0;
                curState = playerState.inLane;
                playerPosition -= 9;
                gameObject.transform.position = new Vector3((float)playerPosition, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        if (curState == playerState.movingRight)
        {
            // increment timer
            timer += Time.deltaTime;
			// if the player is not far enough to the right, move them right
            if (gameObject.transform.position.x <= (9 + playerPosition))
            {
                gameObject.transform.Translate(new Vector3(Time.deltaTime * (9f / 0.1f), 0, 0));
            }
			// if the game object is too far right, move them back
            else
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
