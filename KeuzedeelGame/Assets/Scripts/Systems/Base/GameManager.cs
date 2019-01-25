using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EventSystem))]
[RequireComponent(typeof(GameStateManager))]
[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(WaveSpawnController))]
[RequireComponent(typeof(PauseMenuManager))]
[RequireComponent(typeof(SlowMode))]
public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;
    public ObjectPool m_ObjectPool { get; private set; }
    public EventSystem m_EventSystem { get; private set; }
    private GameStateManager m_GameStateManager;
    private PauseMenuManager m_PauseMenuManager;
    private ShowDeathShadow m_ShowDeathShadow;
    private WaveSpawnController m_WaveSpawnController;
    private SlowMode m_SlowMode;

    private GameObject m_HeadTrigger;
    [SerializeField]
    private GameObject m_LeftHand;
    [SerializeField]
    private GameObject m_RighHand;
    private GameObject m_GameOverScreen;
    private GameObject m_LevelCompleteScreen;
    private GameObject m_ScreenPosition;

    private float m_PauseConfirmTime;
    private float m_PauseDelay;

    private int m_BlocksToComplete;
    private int m_BlocksCompleted;

    private void Awake()
    {
        GetAllSystemScripts();
        SetAllSystemScriptsVariables();
        m_HeadTrigger = GameObject.Find("HeadTrigger");
        m_GameOverScreen = GameObject.Find("GameOverScreen");
        m_LevelCompleteScreen = GameObject.Find("LevelCompleteScreen");
        m_ScreenPosition = GameObject.Find("ScreenPosition");
        m_GameOverScreen.SetActive(false);
        m_LevelCompleteScreen.SetActive(false);
        m_ScreenPosition.SetActive(false);
        m_Instance = this;
        m_PauseDelay = 0f;
        m_PauseConfirmTime = 0.5f;
    }

    private void GetAllSystemScripts()
    {
        m_EventSystem = GetComponent<EventSystem>();
        m_GameStateManager = GetComponent<GameStateManager>();
        m_PauseMenuManager = GetComponent<PauseMenuManager>();
        m_WaveSpawnController = GetComponent<WaveSpawnController>();
        m_ObjectPool = GetComponent<ObjectPool>();
        m_ShowDeathShadow = GetComponent<ShowDeathShadow>();
        m_SlowMode = GetComponent<SlowMode>();
    }

    private void SetAllSystemScriptsVariables()
    {
        m_GameStateManager.SetVariables();
        m_PauseMenuManager.SetVariables();
        m_WaveSpawnController.SetVariables();
        m_ObjectPool.SetVariables();
    }

    private void Start()
    {
        SubToEvents();
        GetAllSystemScriptsRefs();
    }

    private void SubToEvents()
    {
        m_EventSystem.E_HitBlockEvent += GameOver;
        m_EventSystem.E_UnpauseGameEvent += HideViveCamera;
    }

    private void GetAllSystemScriptsRefs()
    {
        m_PauseMenuManager.GetRefs();
        m_WaveSpawnController.GetRefs();
        
    }

    public void LowerPauseConfirmTime()
    {
        m_PauseConfirmTime -= Time.deltaTime;
    }

    public void SetPauseConfirmTime()
    {
        m_PauseConfirmTime = 0.5f;
    }

    public float GetPauseConfirmTime()
    {
        return m_PauseConfirmTime;
    }

    public void SetPauseDelay()
    {
        m_PauseDelay = 1f;
    }

    public float GetPauseDelay()
    {
        return m_PauseDelay;
    }

    public void SlowDownTime()
    {
        m_SlowMode.SlowDownTime();
    }

    public void SpeedUpTime()
    {
        m_SlowMode.SpeedUpTime();
    }

    public void SwitchGameSpeed()
    {
        m_SlowMode.SwitchGameSpeed();
    }

    public void SetAmountOfBlocks(int blocks)
    {
        m_BlocksToComplete = blocks;
    }

    public void SetupDeathScene(string hitObject, GameObject hitWall)
    {
        m_ShowDeathShadow.SetupDeathScene(hitObject, m_HeadTrigger.transform.position, m_HeadTrigger.transform.eulerAngles,
            m_LeftHand.transform.position, m_LeftHand.transform.eulerAngles, m_RighHand.transform.position, m_RighHand.transform.eulerAngles, hitWall);
    }

    public GameState GetGameState()
    {
        return m_GameStateManager.GetGameState();
    }

    public void ChangeGameState(GameState newGameState)
    {
        m_GameStateManager.ChangeGameState(newGameState);
        switch (newGameState)
        {
            case GameState.Running:
                m_EventSystem.UnpauseGame();
                break;
            case GameState.Paused:
                m_EventSystem.PauseGame();
                m_PauseDelay = 1f;
                break;
            case GameState.Completed:
                m_EventSystem.LevelCompleted();
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void CheckIfCanUnpause()
    {
        m_PauseMenuManager.CheckIfCanUnpause();
    }

    public void Unpause()
    {
        ChangeGameState(GameState.Running);
        m_EventSystem.UnpauseGame();
    }

    public void ToggleViveCamera()
    {
        m_GameStateManager.ToggleViveCameraState();
        if (m_GameStateManager.GetViveCameraState() == ViveCameraState.Disabled)
            m_EventSystem.HideViveCamScreen();
        else if (m_GameStateManager.GetViveCameraState() == ViveCameraState.Enabled)
            m_EventSystem.ShowViveCamScreen();
    }

    private void Update()
    {
        if (GetGameState() == GameState.GameOver)
        {
            m_GameOverScreen.transform.position = Vector3.Lerp(m_GameOverScreen.transform.position, m_ScreenPosition.transform.position, 1.5f * Time.deltaTime);
            m_GameOverScreen.transform.LookAt(m_HeadTrigger.transform.position);
        }
        else if (GetGameState() == GameState.Completed)
        {
            m_LevelCompleteScreen.transform.position = Vector3.Lerp(m_LevelCompleteScreen.transform.position, m_ScreenPosition.transform.position, 1.5f * Time.deltaTime);
            m_LevelCompleteScreen.transform.LookAt(m_HeadTrigger.transform.position);
        }

        if (GetGameState() == GameState.Running)
        {
            if (m_PauseDelay >= 0)
            {
                m_PauseDelay -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public float GetGameSpeed()
    {
        return m_SlowMode.GetGameSpeed();
    }

    public GameObject GetHeadTrigger()
    {
        return m_HeadTrigger;
    }

    #region Game States
    public void LevelCompleted()
    {
        ChangeGameState(GameState.Completed);
        m_LevelCompleteScreen.SetActive(true);
        m_ScreenPosition.SetActive(true);
    }

    public void GameOver()
    {
        ChangeGameState(GameState.GameOver);
        m_GameOverScreen.SetActive(true);
        m_ScreenPosition.SetActive(true);
    }
    #endregion

    #region LevelMethods
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region ViveCamera
    public void ShowViveCamera()
    {
        m_EventSystem.ShowViveCamScreen();
    }

    public void HideViveCamera()
    {
        m_EventSystem.HideViveCamScreen();
    }
    #endregion

    public void BlockCompleted()
    {
        m_BlocksCompleted++;
        if (m_BlocksCompleted == m_BlocksToComplete)
        {
            LevelCompleted();
        }
    }
}
