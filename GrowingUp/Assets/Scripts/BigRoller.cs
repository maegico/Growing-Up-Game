using UnityEngine;
using System.Collections;

public class BigRoller : Rotate {

	// handled in parent Rotate.cs
	//------------------------------
	//public Vector3 RotationAxis;
	//public Vector3 InitialRotation;
	//public float RotationSpeed; 
	//public float rotated = 0;

	// hit timer
	public float timer;
	public float rollerMultiplier;

	//Variables for handling behavior when the player is hit
	public bool playerHit;

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

	protected override void Start() {
		PlayerHit = false;
		rollerMultiplier = 0;
	}


	
	// Update is called once per frame
	protected override void Update () {

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

		// debug 
		rotatedDebug = rotated;

		// rotate using value based on time
		transform.rotation = Quaternion.Euler(-rotated*RotationAxis + InitialRotation);
		//transform.rotation = Quaternion.Euler(new Vector3(-rotated,0,90));

		// on-screen rotation is tied to the value
	}
}
