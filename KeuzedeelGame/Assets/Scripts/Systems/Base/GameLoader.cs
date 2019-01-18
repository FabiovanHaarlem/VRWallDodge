using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
	void Start ()
    {
        SavedOptions.m_AudioManager = GetComponent<AudioManager>();
        SetupSettings();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("MainMenu");
	}

    private void SetupSettings()
    {
        SavedOptions.m_MasterVolume = 1.0f;
        SavedOptions.m_MusicVolume = 1.0f;
        SavedOptions.m_EffectsVolume = 1.0f;
    }
}
