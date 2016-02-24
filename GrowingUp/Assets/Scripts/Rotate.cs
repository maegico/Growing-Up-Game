using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// number of times per second that the rotator goes around
    public float RotationSpeed; // a speed of 0.25 makes the wheel rotate once every 4 seconds
	
	// expected framerate to offset deltatime
	//protected float frameRateCoefficient = 60f;

	// number of rotation units per full rotation, in this case degrees
	protected float cycleCoefficient = 360f;

	// total distance in terms of cycles -currently actually in terms of degrees
	public float rotated = 0;

	public int CyclesCompleted {
		get {
			return (int)rotated;
		}
	}

	public float DistanceRotated {
		get {	
			return rotated;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// update total
		rotated += RotationSpeed * cycleCoefficient * Time.deltaTime;
		// rotate using value based on time
		transform.rotation = Quaternion.Euler(new Vector3(-rotated,0,90));

		// on-screen rotation is tied to the value
	}
}
