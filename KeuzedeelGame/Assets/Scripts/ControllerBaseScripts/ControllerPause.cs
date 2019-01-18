using UnityEngine;

public class ControllerPause : MonoBehaviour
{
    private ControllerInput m_ControllerInput;
    private SphereCollider m_Collider;
    private float m_NormaleColliderSize;

    private void Awake()
    {
        m_Collider = GetComponent<SphereCollider>();
        m_ControllerInput = GetComponent<ControllerInput>();
        m_NormaleColliderSize = m_Collider.radius;
    }

    private void Start()
    {
        m_ControllerInput.E_OnTrackpadPressEvent += PauseAndUnpauseGame;
        m_ControllerInput.E_OnTrackpadPressUpEvent += ResetPauseDelay;
        m_ControllerInput.E_OnTriggerDownEvent += ToggleViveCamera;
        m_ControllerInput.E_OnTriggerDownEvent += ChangeGameSpeed;

        GameManager.m_Instance.m_EventSystem.E_PauseGameEvent += DownColliderSize;
        GameManager.m_Instance.m_EventSystem.E_UnpauseGameEvent += UpColliderSize;
        GameManager.m_Instance.m_EventSystem.E_GameOver += DownColliderSize;
        GameManager.m_Instance.m_EventSystem.E_LevelCompleted += DownColliderSize;
    }

    private void DownColliderSize()
    {
        m_Collider.radius = 0.03f;
    }

    private void UpColliderSize()
    {
        m_Collider.radius = m_NormaleColliderSize;
    }

    public void ResetPauseDelay()
    {
        GameManager.m_Instance.SetPauseConfirmTime();
    }

    public void ChangeGameSpeed()
    {
        GameManager.m_Instance.SwitchGameSpeed();
    }

    public void PauseAndUnpauseGame()
    {
        switch (GameManager.m_Instance.GetGameState())
        {
            case GameState.Running:
                if (GameManager.m_Instance.GetPauseDelay() <= 0f)
                {
                    GameManager.m_Instance.LowerPauseConfirmTime();
                    if (GameManager.m_Instance.GetPauseConfirmTime() <= 0f)
                    {
                        GameManager.m_Instance.ChangeGameState(GameState.Paused);
                    }
                }
                break;
            case GameState.Paused:
                if (GameManager.m_Instance.GetPauseConfirmTime() >= 0f)
                {
                    GameManager.m_Instance.CheckIfCanUnpause();
                }
                break;
        }
    }

    public void ToggleViveCamera()
    {
        if (GameManager.m_Instance.GetGameState() == GameState.Paused)
        {
            GameManager.m_Instance.ToggleViveCamera();
        }
    }

    private void PauseGame()
    {
        GameManager.m_Instance.ChangeGameState(GameState.Paused);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.m_Instance.GetGameState() != GameState.Running)
        {
            if (other.CompareTag("PauseMenu"))
            {
                if (other.name == "Resume")
                {
                    Debug.Log("Resume");
                    GameManager.m_Instance.CheckIfCanUnpause();
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
