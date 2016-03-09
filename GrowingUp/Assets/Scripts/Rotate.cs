﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    // rotation axis
    public Vector3 RotationAxis;

    // what rotation is applied first
    public Vector3 InitialRotation;

	// number of times per second that the rotator goes around
    public float RotationSpeed; // a speed of 0.25 makes the wheel rotate once every 4 seconds
	
	// expected framerate to offset deltatime
	//protected float frameRateCoefficient = 60f;

	// number of rotation units per full rotation, in this case degrees
	protected float cycleCoefficient = 360f;

	// total distance in terms of cycles - currently actually in terms of degrees
    /// <summary>
    /// Current distance in terms of degrees.
    /// </summary>
	public float rotated = 0;

    /// <summary>
    /// Returns the number of cycles completed.
    /// </summary>
	public int CyclesCompleted {
		get {
			return (int)rotated/360;
		}
	}

    /// <summary>
    /// Returns the ditance the wheel has rotated in degrees
    /// </summary>
	public float DistanceRotated {
		get {	
			return rotated;
		}
	}

    // Use this for initialization
    protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        // update total
        rotated += Mathf.Abs(RotationSpeed * cycleCoefficient * Time.deltaTime);
    
		// rotate using value based on time
        transform.rotation = Quaternion.Euler(-rotated*RotationAxis + InitialRotation);
        //transform.rotation = Quaternion.Euler(new Vector3(-rotated,0,90));

		// on-screen rotation is tied to the value
	}
}
