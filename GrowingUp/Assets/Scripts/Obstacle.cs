using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Location.lane currentLane;
    public float timer;

	// Use this for initialization
	void Start () {
        //will make it so that obstacle generate sets these
        timer = 3;
        currentLane = Location.lane.middle;
        int lane = (int)(Random.Range(0, 2));
        switch(lane)
        {
            case 0:
                currentLane = Location.lane.left;
                break;
            case 1:
                currentLane = Location.lane.middle;
                break;
            case 2:
                currentLane = Location.lane.right;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
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

            timer = 3;
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
}
