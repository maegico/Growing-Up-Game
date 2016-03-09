using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Location.lane currentLane;
	public float changeLaneTrigger; // 180 means it changes lanes when it hits the bottom of the wheel

	// the position of the obstacle on the wheel
	// measured in degrees from the top (zero)
	public float positionOnWheel;

	protected BigRoller wheel;

	protected GameManagerInit manager;

	// Use this for initialization
	void Start () {
        // set initial states
		manager = GameObject.Find("GameManager").GetComponent<GameManagerInit>();
		wheel = manager.Wheel;
        currentLane = Location.lane.middle;

        // Set initial lane
        SetRandomLane();
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
		
    // Sets a random lane
    void SetRandomLane()
    {
        Location.lane newLane = (Location.lane)Random.Range(0, 3); // decides where the new lane will be
        gameObject.transform.Translate(new Vector3(9 * ((float)newLane - (float)currentLane), 0, 0)); // shift the object to the new lane
        currentLane = newLane; // set the current lane to be the new lane
    }
}
