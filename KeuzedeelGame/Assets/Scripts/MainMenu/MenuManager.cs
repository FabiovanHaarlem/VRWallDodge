using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager m_Instance;

    [SerializeField]
    private List<SOLevelValue> m_LevelsData;

    [SerializeField]
    private ViveWebcam m_ViveWebcam;

    [SerializeField]
    private List<GameObject> m_LevelGroups;
    private int m_LevelGroupIndex;

    private bool m_ViveCamActive;

    private void Awake()
    {
        m_Instance = this;
        SelectedLevelData.SetLevelsData(m_LevelsData);
        m_ViveCamActive = false;
        m_LevelGroupIndex = 0;
    }

    public void ToggleViveCam()
    {
        if (m_ViveCamActive)
        {
            m_ViveWebcam.DisableCamera();
            m_ViveCamActive = false;
        }
        else
        {
            m_ViveWebcam.EnableCamera();
            m_ViveCamActive = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RaiseVolume("Music");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LowerVolume("Music");
        }
    }

    public void GoToLevel(string sceneName)
    {
        SelectedLevelData.SetActiveLevelData(sceneName);
        SceneManager.LoadScene("NormaleMode");
    }
    
    public void SwitchLevelGroup()
    {
        m_LevelGroups[m_LevelGroupIndex].SetActive(false);
        if (m_LevelGroupIndex == m_LevelGroups.Count - 1)
        {
            m_LevelGroupIndex = 0;
        }
        else
        {
            m_LevelGroupIndex++;
        }
        m_LevelGroups[m_LevelGroupIndex].SetActive(true);
    }

    public void RaiseVolume(string soundSystem)
    {
        if (soundSystem == "Master")
        {
            SavedOptions.m_MasterVolume += 0.1f;
            SavedOptions.m_MasterVolume = Mathf.Clamp(SavedOptions.m_MasterVolume, 0.0f, 1.0f);
        }
        else if (soundSystem == "Music")
        {
            SavedOptions.m_MusicVolume += 0.1f;
            SavedOptions.m_MusicVolume = Mathf.Clamp(SavedOptions.m_MusicVolume, 0.0f, 1.0f);
        }
        else if (soundSystem == "Effects")
        {
            SavedOptions.m_EffectsVolume += 0.1f;
            SavedOptions.m_EffectsVolume = Mathf.Clamp(SavedOptions.m_EffectsVolume, 0.0f, 1.0f);
        }

        UpdateVolume();
    }

    public void LowerVolume(string soundSystem)
    {
        if (soundSystem == "Master")
        {
            SavedOptions.m_MasterVolume -= 0.1f;
            SavedOptions.m_MasterVolume = Mathf.Clamp(SavedOptions.m_MasterVolume, 0.0f, 1.0f);
        }
        else if (soundSystem == "Music")
        {
            SavedOptions.m_MusicVolume -= 0.1f;
            SavedOptions.m_MusicVolume = Mathf.Clamp(SavedOptions.m_MusicVolume, 0.0f, 1.0f);
        }
        else if (soundSystem == "Effects")
        {
            SavedOptions.m_EffectsVolume -= 0.1f;
            SavedOptions.m_EffectsVolume = Mathf.Clamp(SavedOptions.m_EffectsVolume, 0.0f, 1.0f);
        }

        UpdateVolume();
    }

    public void UpdateVolume()
    {
        SavedOptions.m_AudioManager.UpdateAudioVolume();
    }
}
