using UnityEngine;
using System.Collections;

public class GameManagerInit : MonoBehaviour {

    PlayerScript player;
	ObstacleGenerator generator;
	BigRoller wheel;
	LevelSelect levelSelect;

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

    void HitPlayer()
    {
        if (player.beenHit == false)
        {
            wheel.PlayerHit = true;
            player.HitPlayer();
        }
    }
}
