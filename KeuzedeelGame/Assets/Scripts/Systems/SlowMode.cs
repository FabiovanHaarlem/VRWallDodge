using UnityEngine;

public class SlowMode : MonoBehaviour
{
    public delegate void SlowTimeEvent();
    public event SlowTimeEvent E_SlowTimeEvent;

    public delegate void NormaleTimeEvent();
    public event NormaleTimeEvent E_NormaleTimeEvent;

    private float m_GameNormaleSpeed;
    private float m_GameSlowedSpeed;
    private float m_GameSpeed;

    private void Awake()
    {
        m_GameNormaleSpeed = Time.timeScale;
        m_GameSpeed = m_GameNormaleSpeed;
        m_GameSlowedSpeed = 0.4f;
    }

    public void SlowDownTime()
    {

    }
}
