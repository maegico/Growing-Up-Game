using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroUI : MonoBehaviour {
	
	public Text[] dialogElements;
	public float[] dialogStartTimes; // this array must at least one longer than dialogElements
	protected float dialogTimer;

	// Use this for initialization
	void Start () {
		foreach (Text txt in dialogElements) {
			txt.color = new Color (txt.color.r, txt.color.g, txt.color.b, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		dialogTimer += Time.deltaTime;
		print (dialogTimer);
		for (int i=0; i<dialogElements.Length; i++) {
			dialogElements [i].color = new Color(
				dialogElements [i].color.r,
				dialogElements [i].color.g,
				dialogElements [i].color.b,
				(dialogTimer - dialogStartTimes [i]) / (dialogStartTimes [i + 1] - dialogStartTimes [i])
			);
		}
	}
}
