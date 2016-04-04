using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour 
{

	public float minSwipeDistY;

	public float minSwipeDistX;

	public Text Dbug;

	private Vector2 startPos;

	private PlayerScript player;

	void Start() {
		player = GetComponent<PlayerScript>();
	}

	void Update()
	{
		//#if UNITY_ANDROID
		if (Input.touchCount > 0) 

		{

			Touch touch = Input.touches[0];

			// move player
			int touchLane = (int)Mathf.Floor(3*touch.position.x/Screen.width);
			//Dbug.text = "touchLane: " + touchLane+" playerlane: " + (int)player.currentLane;

			// left
			if ( (int)player.currentLane > touchLane) {
				player.MoveLeft();
				if ( (int)player.currentLane > touchLane) {
					player.MoveLeft();
				}
			}
			// right
			if ( (int)player.currentLane < touchLane) {
					player.MoveRight();
					if ( (int)player.currentLane < touchLane) {
						player.MoveRight();
					}

			}

			/*


			{

			case TouchPhase.Began:

				startPos = touch.position;

				break;



			case TouchPhase.Ended:

				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

				if (swipeDistVertical > minSwipeDistY) 

				{

					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

					if (swipeValue > 0)//up swipe

						//Jump ();
						player.Jump();

					//else if (swipeValue < 0)//down swipe

						//Shrink ();

				}

				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

				if (swipeDistHorizontal > minSwipeDistX) 

				{

					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

					if (swipeValue > 0)//right swipe

						player.MoveRight();

					else if (swipeValue < 0)//left swipe

						//MoveLeft ();
						player.MoveLeft();
					}
				break;
			}
			*/
		}
	}
}