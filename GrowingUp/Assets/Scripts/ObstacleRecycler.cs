using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Recycles obstacles that are out of sight
public class ObstacleRecycler : MonoBehaviour {

	ObstacleGenerator generator;
	GameManagerInit manager;
	BigRoller roller;

	// how many frames pass between each check
	protected int frameInterval = 10; // check every ten
	protected int frameCounter = 0;

	// Use this for initialization
	void Start () {
		manager = GetComponent<GameManagerInit> ();
		roller = manager.Wheel;
		generator = GetComponent<ObstacleGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// if it is time to check
		print ("ASDF");
		if (frameCounter > frameInterval) {
			// check to see if an obstacle is out of range
			checkForPassedObstacles ();
			frameCounter = 0; // reset counter
		}
		// increment counter
		frameCounter++;
	}

	// checks to see if an obstacle is behind the player and frees up space if it is
	protected void checkForPassedObstacles() {
		print ("ASDF");
		// if "i" exceeds count, end  the loop (the reason we don't do a for loop is because the list is changing length within the loop)
		bool endReached = false;
		// current index, unlike a for loop, the index only increments if the obstacle is NOT expired
		int i = 0;
		// loop through all the obstacles
		while (!endReached) {
			// only continue if i is less than count
			if (i < generator.Obstacles.Count) {
				// if the obstacle is past its killpoint, destroy it and remove it from the list
				if (roller.DistanceRotated > generator.Obstacles [i].KillPoint) {
					// save the doomed obstacle
					Obstacle temp = generator.Obstacles [i];
					// remove from the list
					generator.Obstacles.RemoveAt (i);
					// destroy the gameobject
					GameObject.Destroy (temp.gameObject);
				} else {
					// if the obstacle still has time to live, check the next
					i++;
				}
			} else {
				// if there are no more obstacles, exit
				endReached = true;
			}
		}
	}
}
