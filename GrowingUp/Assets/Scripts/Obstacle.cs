using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Location.lane currentLane;
	public float changeLaneTrigger; // 180 means it changes lanes when it hits the bottom of the wheel

	// the position of the obstacle on the wheel
	// measured in degrees from the top (zero)
	public float positionOnWheel;

	protected Rotate wheel;

	protected GameManagerInit manager;

	// Use this for initialization
	void Start () {
        // set initial states
		manager = GameObject.Find("GameManager").GetComponent<GameManagerInit>();
		wheel = manager.Wheel;
        currentLane = Location.lane.middle;
        // will make it so that obstacle generate sets these
        //timer = 3f;

        // Set initial lane
        SetRandomLane();
        //int lane = Random.Range(0, 2);
        // where the obstacle is located
        // in terms distance from the top of the wheel
        // measured in degrees
        //currentLane = laneFromInt(lane);
        //currentLane = (Location.lane)Random.Range(0, 2);
        //setPositionToCurrentLane(); // on start

        // Set initial distance on wheel
        wheel.rotated += positionOnWheel;
        //changeLaneTrigger += positionOnWheel;
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
            //positionOnWheel = 0;
            //int action = Random.Range(0, 2);

            // Switch to a random lane.
            SetRandomLane();
            //switch (Random.Range(0, 2))
            //{
            //    case 0:
            //        MoveRight();
            //        break;
            //    case 1:
            //        MoveLeft();
            //        break;
            //    default:
            //        break;
            //}

            changeLaneTrigger += 360;
			print (currentLane);
        }


        
    }

    //void MoveRight()
    //{
    //    switch (currentLane)
    //    {
    //        case Location.lane.left:
    //            currentLane = Location.lane.middle;
    //            gameObject.transform.Translate(new Vector3(9, 0, 0));
    //            break;
    //        case Location.lane.middle:
    //            currentLane = Location.lane.right;
    //            gameObject.transform.Translate(new Vector3(9, 0, 0));
    //            break;
    //        case Location.lane.right:
    //            break;
    //        default:
    //            break;
    //    }
    //}
    //
    //void MoveLeft()
    //{
    //    switch (currentLane)
    //    {
    //        case Location.lane.left:
    //            break;
    //        case Location.lane.middle:
    //            currentLane = Location.lane.left;
    //            gameObject.transform.Translate(new Vector3(-9, 0, 0));
    //            break;
    //        case Location.lane.right:
    //            currentLane = Location.lane.middle;
    //            gameObject.transform.Translate(new Vector3(-9, 0, 0));
    //            break;
    //        default:
    //            break;
    //    }
    //
    //}

	// take an int, return a lane enum
	//Location.lane laneFromInt(int laneInt) {
	//	switch(laneInt)
	//	{
	//	case 0:
	//		return Location.lane.left;
	//		break;
	//	case 1:
	//		return Location.lane.middle;
	//		break;
	//	case 2:
	//		return Location.lane.right;
	//		break;
	//	}
	//	return Location.lane.middle;
	//}

	//void setPositionToCurrentLane() {
	//	switch (currentLane)
	//	{
	//	case Location.lane.left:
	//		gameObject.transform.Translate(new Vector3(-9, 0, 0));
	//		break;
	//	case Location.lane.middle:
	//		break;
	//	case Location.lane.right:
	//		gameObject.transform.Translate(new Vector3(9, 0, 0));
	//		break;
	//	default:
	//		break;
	//	}
	//}

    // Sets a random lane
    void SetRandomLane()
    {
        Location.lane newLane = (Location.lane)Random.Range(0, 3); // decides where the new lane will be
        gameObject.transform.Translate(new Vector3(9 * ((float)newLane - (float)currentLane), 0, 0)); // shift the object to the new lane
        currentLane = newLane; // set the current lane to be the new lane
    }
}
