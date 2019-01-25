using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    private GameObject m_LevelSelectMenu;
    [SerializeField]
    private GameObject m_OptionsMenu;
    private GameObject m_ActiveMenu;

    [SerializeField]
    private TextMeshProUGUI m_LevelSelectDiffculty;

    private ChangeLanguage m_ChangeLanguage;
    [SerializeField]
    private GameObject m_WholeMainMenuUI;
    [SerializeField]
    private SpriteRenderer m_FakeGammaSprite;

    private int m_LevelGroupIndex;

    private bool m_ViveCamActive;

    private void Awake()
    {
        m_Instance = this;
        SelectedLevelData.SetLevelsData(m_LevelsData);
        m_ViveCamActive = false;
        m_LevelGroupIndex = 0;
        m_ChangeLanguage = GetComponent<ChangeLanguage>();
        if (m_LevelSelectMenu != null)
            m_ActiveMenu = m_LevelSelectMenu;
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
            QuitGame();
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToLevel(string sceneName)
    {
        SelectedLevelData.SetActiveLevelData(sceneName);
        SceneManager.LoadScene("NormaleMode");
    }

    public void SwitchLanguage()
    {
        m_ChangeLanguage.SwitchLanguage();
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
        
        switch(m_LevelGroupIndex)
        {
            case 0:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_EasyMode;
                break;
            case 1:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_MediumMode;
                break;
            case 2:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_HardMode;
                break;
            case 3:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_SpecialMode;
                break;
        }
        m_LevelGroups[m_LevelGroupIndex].SetActive(true);
    }

    public void GoToOptions()
    {
        m_ActiveMenu.SetActive(false);
        m_ActiveMenu = m_OptionsMenu;
        m_ActiveMenu.SetActive(true);
    }

    public void GoToLevelSelect()
    {
        switch (m_LevelGroupIndex)
        {
            case 0:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_EasyMode;
                break;
            case 1:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_MediumMode;
                break;
            case 2:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_HardMode;
                break;
            case 3:
                m_LevelSelectDiffculty.text = m_ChangeLanguage.GetLanguageSet().m_SpecialMode;
                break;
        }

        m_ActiveMenu.SetActive(false);
        m_ActiveMenu = m_LevelSelectMenu;
        m_ActiveMenu.SetActive(true);
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

    public void UIScaleUp()
    {
        float scaleValue = 0.0005f;
        Vector3 currentScale = m_WholeMainMenuUI.transform.localScale;
        Vector3 newScale = new Vector3(currentScale.x + scaleValue, currentScale.y + scaleValue, currentScale.z);
        m_WholeMainMenuUI.transform.localScale = newScale;
    }

    public void UIScaleDown()
    {
        float scaleValue = 0.0005f;
        Vector3 currentScale = m_WholeMainMenuUI.transform.localScale;
        Vector3 newScale = new Vector3(currentScale.x - scaleValue, currentScale.y - scaleValue, currentScale.z);
        m_WholeMainMenuUI.transform.localScale = newScale;
    }

    public void GammaUp()
    {
        m_FakeGammaSprite.color = new Color(m_FakeGammaSprite.color.r, m_FakeGammaSprite.color.g, m_FakeGammaSprite.color.b, m_FakeGammaSprite.color.a + 0.05f);
    }

    public void GammaDown()
    {
        m_FakeGammaSprite.color = new Color(m_FakeGammaSprite.color.r, m_FakeGammaSprite.color.g, m_FakeGammaSprite.color.b, m_FakeGammaSprite.color.a - 0.05f);
    }
}
