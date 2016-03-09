﻿using UnityEngine;
using System.Collections;

public class GameManagerInit : MonoBehaviour {

    PlayerScript player;
	ObstacleGenerator generator;
	BigRoller wheel;
	LevelSelect levelSelect;

	protected bool obstaclesPregenerated = false;

	public PlayerScript Player{
		get {
			return player;
		}
	}
	public ObstacleGenerator Generator {
		get {
			return generator;
		}
	}
	public BigRoller Wheel {
		get {
			return wheel;
		}
	}

	// Use this for initialization
	void Start () {
		// get player reference
        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerScript>();
		// get generator component
		generator = GetComponent<ObstacleGenerator>(); // assumes the component is on this gameobject
		// get wheel reference
		wheel = GameObject.FindGameObjectWithTag("Roller").GetComponent<BigRoller>();
		// get level select ref
		levelSelect = GetComponent<LevelSelect>();

        // get actual player angle
        player.posOnWheel = Vector3.Angle(Vector3.up, player.transform.position - wheel.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		// run once if everything is loaded
		if (!obstaclesPregenerated) {
			CreateStartingObstacles ();
		}

		// check collisions
		foreach(Obstacle obs in generator.Obstacles) {

			// get the current position of the obstacle
			float obsPos = wheel.DistanceRotated + obs.positionOnWheel;

			// check to see if the obstacle is in line with the player
			if (obsPos%360 > player.posOnWheel && obsPos%360 < player.posOnWheel+wheel.RotationSpeed*Time.deltaTime*360) {

				// check if the obstacle is in the player's lane
				if (obs.currentLane == player.currentLane) {
                    // collision!
                    //levelSelect.MainMenu();
                    HitPlayer();
				}
			}
		}
		//if (player.posOnWheel + 
	}

	protected void CreateStartingObstacles() {
		// if already run, return without doing anything
		if (obstaclesPregenerated) return;
		float startingRotation = 180f;
		float rotationCounter = 0f;
		while (rotationCounter < startingRotation) {
			// spawn distance is the time interval for spawn in seconds times the wheel speed in degrees / second
			float spawnDist = generator.ObstacleSpawnInterval * wheel.RotationSpeed * 360f;
			// rotate one obstacle spawn distance
			wheel.ManualRotate (spawnDist);
			// increment counter
			rotationCounter += spawnDist;
			// create obstacle the normal way
			generator.AddObstacle();
			// distance rotated updates itself
		}
		// on complete
		obstaclesPregenerated = true;
	}

    void HitPlayer()
    {
        if (player.beenHit == false)
        {
            wheel.PlayerHit = true;
            player.HitPlayer();
        }
    }
}
