using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// controls the canvas in the game
public class GameplayUI : MonoBehaviour
{

    public Canvas canvas; // set in inspector
    public GameObject childhoodBar; // set in inspector
    public RawImage[] stressOverlay; // set in inspector
	public Text growingText; // set in inspector
	public float growingTextDuration; // set in inspector how many seconds text grows
	protected float growingTextTimer; // timer for the grow effect
    private float barWidth; // width of the childhood bar

    // this is what you are setting to the date string
    public Text scoreCounter; // set in inspector
    
    // the speed that the date increases
    public float degreesRotatedPerDay; // 360 degrees per circle, 365 days per year
    
    // date is in integer form in terms of number of days since Jan 1 2000
    protected int date = 1; // set in code

    // the year that date starts counting from. 
    // Remember, the wheel starts at 180 degrees, so this may not be the first year the player sees...
    public int startYear = 2000; // set in inspector


    // month names
    public string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
    // month lengths
    public int[] monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    // leap years...

    protected GameManagerInit manager;

    public float BarWidth
    {
        get
        {
            return barWidth;
        }

        set
        {
            barWidth = value;
        }
    }
    

    // Use this for initialization
    void Start()
    {
        manager = GetComponent<GameManagerInit>();
        childhoodBar = GameObject.FindGameObjectWithTag("ChildhoodBar");
        BarWidth = 1.0f;
		growingTextTimer = growingTextDuration + 1;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the date based on the rotation of the wheel (remember, the wheel STARTS at 180 degrees)
        date = (int)(manager.Wheel.DistanceRotated * degreesRotatedPerDay);
        // set the UI to the date
        scoreCounter.text = ConvertIntToDateString(date);

        RectTransform rt = childhoodBar.GetComponent<RectTransform>();
		//rt.sizeDelta = new Vector2 (-4, -4);//rt.rect.height);
		rt.anchorMax = new Vector2(0.5f + BarWidth/2, 1);
		rt.anchorMin = new Vector2 (0.5f - BarWidth/2, 0);
		rt.offsetMin = new Vector2 (2, 2);
		rt.offsetMax = new Vector2 (-2, -2);
		if (growingTextTimer <= growingTextDuration) {
			emergeGrowingText ();
		}
    }

    string ConvertIntToDateString(int d)
    {
        // decide what year it is
        int year = startYear + (int)Mathf.Floor(date / 360);
        bool leapYear = (year % 4 == 0);
        int daysThisYear = date % 360;
        int tally = daysThisYear;
        string month = "";
        int day = 0;
        for (int i = 0; i < monthDays.Length; i++)
        {
            if (tally < monthDays[i])
            {
                month = months[i];
                day = tally;
                break;
            }
            else
            {
                tally -= monthDays[i];

            }
        }
        return (month + " " + day + ", " + year);
    }

	public void showObstacleText(string message) {
		print (message);
		growingText.text = message;
		growingTextTimer = 0;
	}
		
	protected void emergeGrowingText() {
		growingTextTimer += Time.deltaTime;
		growingText.rectTransform.anchorMin = new Vector2(-1*growingTextTimer/growingTextDuration+0.5f,0f);
		growingText.rectTransform.anchorMax = new Vector2(1*growingTextTimer/growingTextDuration+0.5f,1f);
		growingText.color = new Color (growingText.color.r, growingText.color.g, growingText.color.b, 1 - growingTextTimer / growingTextDuration);
	}
}
