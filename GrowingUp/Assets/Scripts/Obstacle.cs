using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Location.lane currentLane;
	public float changeLaneTrigger; // 180 means it changes lanes when it hits the bottom of the wheel

	// the position of the obstacle on the wheel
	// measured in degrees from the top (zero)
	public float positionOnWheel;
	public string[] obsName;

    public Texture2D[] obstacleTextures;
	public float[] obstacleXScales;
    public GameObject obstacleImage;

	protected int prevLifeStage = -1;

	protected BigRoller wheel;

	protected GameManagerInit manager;

	// when the wheel reaches this value, destroy the obstacle
	protected float killPoint = 0f;

	public float KillPoint {
		get {
			return killPoint;
		}
		set {
			killPoint = value;
		}
	}

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

		// when the life stage change occurs
		if (prevLifeStage != manager.currentLifeStage) {
        	obstacleImage.GetComponent<Renderer>().material.SetTexture("_MainTex", obstacleTextures[manager.currentLifeStage]);
			transform.localScale = new Vector3(obstacleXScales[manager.currentLifeStage]/transform.parent.localScale.x,transform.localScale.y,transform.localScale.z);
		}

    }
		
    // Sets a random lane
    void SetRandomLane()
    {
        Location.lane newLane = (Location.lane)Random.Range(0, 3); // decides where the new lane will be
        gameObject.transform.Translate(new Vector3(9 * ((float)newLane - (float)currentLane), 0, 0)); // shift the object to the new lane
        currentLane = newLane; // set the current lane to be the new lane
    }
}
