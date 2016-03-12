using UnityEngine;
using System.Collections;

public class GameManagerInit : MonoBehaviour {

    PlayerScript player;
	ObstacleGenerator generator;
	BigRoller wheel;
	LevelSelect levelSelect;

    #region Audio Work in Progress
    // Audio Variables

    /// <summary>
    /// The audio listener...
    /// </summary>
    public AudioListener ears;


    #endregion

    public float stressDistance;        // Distance "life's stress" is from player
    const float maxDistance = 20;       // Maximum distance "life's stress" can be from the player


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

        stressDistance = maxDistance;
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
        if (!player.beenHit)
        {
            stressDistance += Time.deltaTime;
            if (stressDistance > maxDistance) stressDistance = maxDistance;
        }
        else
        {
            stressDistance -= Time.deltaTime;
            if (stressDistance < 0) stressDistance = 0;
        }
        if (stressDistance < 5) player.playerHealth -= Time.deltaTime;
	}

	// create some obstacles before the game starts so that its not boring
	protected void CreateStartingObstacles() {
		// if already run, return without doing anything
		if (obstaclesPregenerated) return;
		float gameStartRotation = 180f;
		float rotationCounter = 0f;
		// go until the wheel has arrived at the correct starting location for the game
		while (rotationCounter < gameStartRotation) {
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

    #region Background Music

    /// <summary>
    /// Fade the melody in. Test Method.
    /// </summary>
    public void FadeIn()
    {

    }

    /// <summary>
    /// Fade the melody out. Test Method.
    /// </summary>
    public void FadeOut()
    {
        
    }

    #endregion
}
