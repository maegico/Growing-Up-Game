using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Location.lane currentLane;
	public float changeLaneTrigger = 180f; // 180 means it changes lanes when it hits the bottom of the wheel

	// the position of the obstacle on the wheel
	// measured in degrees from the top (zero)
	public float positionOnWheel;

	protected Rotate wheel;

	protected GameManagerInit manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("GameManager").GetComponent<GameManagerInit>();
		wheel = manager.Wheel;
        // will make it so that obstacle generate sets these
        //timer = 3f;

        currentLane = Location.lane.middle;
        int lane = (int)(Random.Range(0, 2));
		// where the obstacle is located
		// in terms distance from the top of the wheel
		// measured in degrees
		positionOnWheel = 0;

		currentLane = laneFromInt(lane);
		setPositionToCurrentLane(); // on start
    }
	
	// Update is called once per frame
	void Update () {
        //timer -= Time.deltaTime;
		// when the timer runs out, switch lanes (temporary)
        if (wheel.rotated >= changeLaneTrigger)
        {
			// give the obstacle a distance value
			// normally this would be set when the obstacle spawns,
			// but since we are not spawning yet, I set it when it
			// changes position
			positionOnWheel = 0;
            int action = (int)(Random.Range(0, 2));
            switch (action)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveLeft();
                    break;
                default:
                    break;
            }

            changeLaneTrigger += 360;
			print (currentLane);
        }


        
    }

    void MoveRight()
    {
        switch (currentLane)
        {
            case Location.lane.left:
                currentLane = Location.lane.middle;
                gameObject.transform.Translate(new Vector3(9, 0, 0));
                break;
            case Location.lane.middle:
                currentLane = Location.lane.right;
                gameObject.transform.Translate(new Vector3(9, 0, 0));
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
                gameObject.transform.Translate(new Vector3(-9, 0, 0));
                break;
            case Location.lane.right:
                currentLane = Location.lane.middle;
                gameObject.transform.Translate(new Vector3(-9, 0, 0));
                break;
            default:
                break;
        }

    }

	// take an int, return a lane enum
	Location.lane laneFromInt(int laneInt) {
		switch(laneInt)
		{
		case 0:
			return Location.lane.left;
			break;
		case 1:
			return Location.lane.middle;
			break;
		case 2:
			return Location.lane.right;
			break;
		}
		return Location.lane.middle;
	}

	void setPositionToCurrentLane() {
		switch (currentLane)
		{
		case Location.lane.left:
			gameObject.transform.Translate(new Vector3(-9, 0, 0));
			break;
		case Location.lane.middle:
			break;
		case Location.lane.right:
			gameObject.transform.Translate(new Vector3(9, 0, 0));
			break;
		default:
			break;
		}
	}
}
