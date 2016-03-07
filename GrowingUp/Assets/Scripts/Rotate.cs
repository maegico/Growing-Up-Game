using UnityEngine;
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

    //Variables for handling behavior when the player is hit
    public bool playerHit;
    public float timer;
    public float rollerMultiplier;

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

    /// <summary>
    /// Allows the gameManager to tell the wheel when the player has been hit
    /// </summary>
    public bool PlayerHit
    {
        get
        {
            return playerHit;
        }

        set
        {
            playerHit = value;
        }
    }

    // Use this for initialization
    void Start () {
        PlayerHit = false;
        rollerMultiplier = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // update total
        if (!playerHit)
        {
            rotated += Mathf.Abs(RotationSpeed * cycleCoefficient * Time.deltaTime);
        }
        //If the player is hit, the wheel rolls backwards, then then stops and spins back up to full speed over the course of 3 seconds
        else
        {
            timer += Time.deltaTime;
            rollerMultiplier = -1 * Mathf.Cos(Mathf.PI * timer / 3);
            rotated += rollerMultiplier * Mathf.Abs(RotationSpeed * cycleCoefficient * Time.deltaTime);
            if (timer > 3)
            {
                playerHit = false;
                timer = 0;
                rollerMultiplier = 0;
            }
        }
		// rotate using value based on time
        transform.rotation = Quaternion.Euler(-rotated*RotationAxis + InitialRotation);
        //transform.rotation = Quaternion.Euler(new Vector3(-rotated,0,90));

		// on-screen rotation is tied to the value
	}
}
