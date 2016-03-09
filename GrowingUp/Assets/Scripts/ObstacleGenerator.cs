using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour
{

    //(1.042, 0.021, 0) - child of roller

    float timer;
    List<Obstacle> obstacles;
    public Object obstaclePrefab;
	protected int maxObstacles = 50;
	protected Quaternion flipQuat;
	protected Vector3 obstacleSpawnLocation;
	BigRoller roller;
	protected float playerOffset;
	public float ObstacleSpawnInterval; // set in inspector

    //GameManagerInit gm;

    public List<Obstacle> Obstacles
    {
        get
        {
            return obstacles;
        }
    }

    // Use this for initialization
    void Start()
    {
        //gm = GetComponent<GameManagerInit>();
		timer = ObstacleSpawnInterval;
        obstacles = new List<Obstacle>();
        // for now we'll fill this list with the one obstacle in the game
		if (GameObject.FindObjectOfType<Obstacle>() != null) obstacles.Add(GameObject.FindObjectOfType<Obstacle>());
		obstacleSpawnLocation = new Vector3 (0, -981.5f, 1.05f);
		flipQuat = Quaternion.Euler (180, 0, 0);
		roller = GetComponent<GameManagerInit> ().Wheel;
		playerOffset = GetComponent<GameManagerInit> ().Player.posOnWheel;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
		if (obstacles.Count < maxObstacles && timer <= 0)
        {
			//Generate Obstacle
			AddObstacle ();
            
            // reset timer
			timer = ObstacleSpawnInterval;
        }
    }

	public Obstacle AddObstacle() {
		// create a new obstacle
		Object temp = Instantiate(obstaclePrefab, obstacleSpawnLocation, flipQuat);
		// get the Obstacle component
		Obstacle newObstacle = ((GameObject)temp).GetComponent<Obstacle>();
		// parent the obstacle to the wheel
		newObstacle.transform.SetParent(roller.transform);
		// tell the obstacle its "kill point" after it passes the player
		newObstacle.KillPoint = roller.DistanceRotated + 220f;
		// determine where the obstacle appears
		float angleOfOriginalWheelTop = roller.DistanceRotated%360;
		float mysteryOffset = 11f; // there's a weird offset bug so I'm just going to hardcode a value for now
		// interestingly, this offset is equal to the player's distance from the top of the wheel,
		// but the in theory the player position is already factored into the calculation...
		// set the obstacle's position on wheel value to the position on the wheel where it spawned
		newObstacle.positionOnWheel = (180 - mysteryOffset - angleOfOriginalWheelTop)%360;
		// add obstacle to the list
		obstacles.Add(newObstacle);
		// return
		return newObstacle;
	}
}