using UnityEngine;

public class IngameControllers : MonoBehaviour
{
    private SteamVR_TrackedObject m_TrackedObject;
    private BoxCollider m_Collider;
    private Vector3 m_NormaleColliderSize;

    private SteamVR_Controller.Device m_Controller
    {
        get { return SteamVR_Controller.Input((int)m_TrackedObject.index); }
    }

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider>();
        m_TrackedObject = GetComponent<SteamVR_TrackedObject>();
        m_NormaleColliderSize = m_Collider.size;
    }

    private void Start()
    {
        GameManager.m_Instance.m_EventSystem.E_PauseGameEvent += DownColliderSize;
        GameManager.m_Instance.m_EventSystem.E_UnpauseGameEvent += UpColliderSize;
        GameManager.m_Instance.m_EventSystem.E_GameOver += DownColliderSize;
        GameManager.m_Instance.m_EventSystem.E_LevelCompleted += DownColliderSize;
    }

    private void DownColliderSize()
    {
        m_Collider.size = new Vector3(0.05f, 0.05f, 0.05f);
        m_Collider.center = new Vector3(0f, -0.03f, 0.02f);
    }

    private void UpColliderSize()
    {
        m_Collider.size = m_NormaleColliderSize;
        m_Collider.center = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        if (m_Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            switch(GameManager.m_Instance.GetGameState())
            {
                case GameState.Running:
                    GameManager.m_Instance.ChangeGameState(GameState.Paused);
                    break;
                case GameState.Paused:
                    GameManager.m_Instance.CheckIfCanUnpause();
                    break;
            }
        }

        if (GameManager.m_Instance.GetGameState() == GameState.Paused)
        {
            if (m_Controller.GetHairTriggerDown())
            {
                GameManager.m_Instance.ToggleViveCamera();
            }
        }
    }

    private void PauseGame()
    {
        GameManager.m_Instance.ChangeGameState(GameState.Paused);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.m_Instance.m_EventSystem.HitBlock();
        }
        
        if (GameManager.m_Instance.GetGameState() != GameState.Running)
        {
            if (other.CompareTag("PauseMenu"))
            {
                if (other.name == "Resume")
                {
                    GameManager.m_Instance.m_EventSystem.UnpauseGame();
                }
                else if (other.name == "ExitLevel")
                {
                    GameManager.m_Instance.BackToMainMenu();
                }
                else if (other.name == "RestartLevel")
                {
                    GameManager.m_Instance.RestartLevel();
                }
            }
        }
    }
}
