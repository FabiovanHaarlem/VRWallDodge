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

    private bool m_ViveCamActive;

    private void Awake()
    {
        m_Instance = this;
        SelectedLevelData.SetLevelsData(m_LevelsData);
        m_ViveCamActive = false;
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
    }

    public void GoToLevel(string sceneName)
    {
        SelectedLevelData.SetActiveLevelData(sceneName);
        SceneManager.LoadScene("NormaleMode");
    }


    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
