using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// controls the canvas in the game
public class GameplayUI : MonoBehaviour
{

    public Canvas canvas; // set in inspector
    public GameObject childhoodBar; // set in inspector
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
        BarWidth = 96;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the date based on the rotation of the wheel (remember, the wheel STARTS at 180 degrees)
        date = (int)(manager.Wheel.DistanceRotated * degreesRotatedPerDay);
        // set the UI to the date
        scoreCounter.text = ConvertIntToDateString(date);

        RectTransform rt = childhoodBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(BarWidth, rt.rect.height);
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
}
