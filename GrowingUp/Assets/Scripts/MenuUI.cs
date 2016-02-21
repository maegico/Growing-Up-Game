using UnityEngine;
using System.Collections;

public class MenuUI : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject creditsScreen;
    public GameObject instructionsScreen;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void HideAll()
    {
        titleScreen.SetActive(false);
        creditsScreen.SetActive(false);
        instructionsScreen.SetActive(false);
    }

    public void ShowCredits()
    {
        HideAll();
        creditsScreen.SetActive(true);
    }

    public void ShowTitle()
    {
        HideAll();
        titleScreen.SetActive(true);
    }

    public void ShowInstructions()
    {
        HideAll();
        instructionsScreen.SetActive(true);
    }
}
