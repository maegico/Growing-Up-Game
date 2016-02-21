using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    Location.lane currentLane;

    
    
	// Use this for initialization
	void Start () {
        currentLane = Location.lane.middle;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
    }

    void MoveRight()
    {
        switch  (currentLane)
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
