using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioControl : MonoBehaviour {

    public AudioMixer mixer;

    // Use this for initialization
    void Start()
    {
    }

	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Modify master volume
    /// </summary>
    /// <param name="level">Volume level /1</param>
    public void SetMasterVol(float level)
    {
        if (level != 0f) mixer.SetFloat("masterVol", 20f * Mathf.Log10(level));
        else mixer.SetFloat("masterVol", -80.0f);
    }

    /// <summary>
    /// Modify music volume
    /// </summary>
    /// <param name="level">Volume level /1</param>
    public void SetMusicVol(float level)
    {
        if (level != 0f) mixer.SetFloat("musicVol", 20f * Mathf.Log10(level));
        else mixer.SetFloat("musicVol", -80.0f);
    }

    /// <summary>
    /// Modify melody volume - test Method
    /// </summary>
    /// <param name="level">Volume level /1</param>
    public void SetMelodyVol(float level)
    {
        if (level != 0f) mixer.SetFloat("melodyVol", 20f * Mathf.Log10(level));
        else mixer.SetFloat("melodyVol", -80.0f);
    }

    /// <summary>
    /// Modify master volume
    /// </summary>
    /// <param name="level">Volume level /1</param>
    public void SetTeenVol(float level)
    {
        if (level != 0f) mixer.SetFloat("teenVol", 20f * Mathf.Log10(level));
        else mixer.SetFloat("teenVol", -80.0f);
    }

    /// <summary>
    /// Modify master volume
    /// </summary>
    /// <param name="level">Volume level /1</param>
    public void SetAdultVol(float level)
    {
        if (level != 0f) mixer.SetFloat("adultVol", 20f * Mathf.Log10(level));
        else mixer.SetFloat("adultVol", -80.0f);
    }

    /// <summary>
    /// Modify sfx volume
    /// </summary>
    /// <param name="level">Volume level /1</param>
    public void SetSFXVol(float level)
    {
        if (level != 0f) mixer.SetFloat("sfxVol", 20f * Mathf.Log10(level));
        else mixer.SetFloat("sfxVol", -80.0f);
    }
}
