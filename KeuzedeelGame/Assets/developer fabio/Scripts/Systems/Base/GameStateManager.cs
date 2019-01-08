using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameState m_GameState;
    private ViveCameraState m_ViveCameraState;

    //Sets Variables
    public void SetVariables()
    {
        m_GameState = GameState.Running;
        m_ViveCameraState = ViveCameraState.Disabled;
    }

    public GameState GetGameState()
    {
        return m_GameState;
    }

    public ViveCameraState GetViveCameraState()
    {
        return m_ViveCameraState;
    }

    public void ChangeGameState(GameState newGameState)
    {
        m_GameState = newGameState;
    }

    public void ToggleViveCameraState()
    {
        if (m_ViveCameraState == ViveCameraState.Disabled)
            m_ViveCameraState = ViveCameraState.Enabled;
        else if (m_ViveCameraState == ViveCameraState.Enabled)
            m_ViveCameraState = ViveCameraState.Disabled;
    }
}

public enum GameState
{
    Running = 0,
    Paused,
    Completed,
    GameOver
}

public enum ViveCameraState
{
    Enabled = 0,
    Disabled
}
