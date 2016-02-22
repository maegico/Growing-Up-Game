using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    enum playerState
    {
        inLane,
        movingLeft,
        movingRight,
        jumping
    }

    playerState curState;
    Location.lane currentLane;
    float timer;
    int playerPosition;
    
    
	// Use this for initialization
	void Start () {
        currentLane = Location.lane.middle;
        timer = 0;
        playerPosition = 0;
        curState = playerState.inLane;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && curState == playerState.inLane) MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow) && curState == playerState.inLane) MoveRight();
        if (curState == playerState.movingLeft)
        {
            float deltaTime = Time.deltaTime;
            timer += deltaTime;
            if (gameObject.transform.position.x >= (-9 + playerPosition))
            {
                gameObject.transform.Translate(new Vector3(-deltaTime * (9f / 0.1f), 0, 0));
            }
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
            float deltaTime = Time.deltaTime;
            timer += deltaTime;
            if (gameObject.transform.position.x <= (9 + playerPosition))
            {
                gameObject.transform.Translate(new Vector3(deltaTime * (9f / 0.1f), 0, 0));
            }
            else
            {
                timer = 0;
                curState = playerState.inLane;
                playerPosition += 9;
                gameObject.transform.position = new Vector3((float)playerPosition, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }

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
