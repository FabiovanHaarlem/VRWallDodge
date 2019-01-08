using UnityEngine;

public class EventSystem : MonoBehaviour
{
    #region Game States Events
    public delegate void PauseGameEvent();
    public event PauseGameEvent E_PauseGameEvent;
    public void PauseGame()
    {
        E_PauseGameEvent.Invoke();
    }

    public delegate void UnpauseGameEvent();
    public event UnpauseGameEvent E_UnpauseGameEvent;
    public void UnpauseGame()
    {
        E_UnpauseGameEvent.Invoke();
    }

    public delegate void GameOverEvent();
    public event GameOverEvent E_GameOver;
    public void GameOver()
    {
        E_GameOver.Invoke();
    }

    public delegate void LevelCompletedEvent();
    public event LevelCompletedEvent E_LevelCompleted;
    public void LevelCompleted()
    {
        E_LevelCompleted.Invoke();
    }
    #endregion

    #region Ingame Events
    public delegate void HitBlockEvent();
    public event HitBlockEvent E_HitBlockEvent;
    public void HitBlock()
    {
        E_HitBlockEvent.Invoke();
    }

    public delegate void PassedWallEvent();
    public event PassedWallEvent E_PassedWallEvent;
    public void PassedWall()
    {
        E_PassedWallEvent.Invoke();
    }
    #endregion

    #region CameraEvents
    public delegate void ShowViveCamScreenEvent();
    public event ShowViveCamScreenEvent E_ShowViveCamScreen;
    public void ShowViveCamScreen()
    {
        E_ShowViveCamScreen.Invoke();
    }

    public delegate void HideViveCamScreenEvent();
    public event HideViveCamScreenEvent E_HideViveCamScreen;
    public void HideViveCamScreen()
    {
        E_HideViveCamScreen.Invoke();
    }
    #endregion
}
