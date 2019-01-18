using UnityEngine;

public class SlowMode : MonoBehaviour
{
    private GameSpeed m_CurrentGameSpeed;
    private float m_SlowTimeLeft;
    private float m_GameSpeedChange;

    private float m_GameNormaleSpeed;
    private float m_GameSlowedSpeed;
    private float m_GameSpeed;

    private void Awake()
    {
        m_GameNormaleSpeed = Time.timeScale;
        m_GameSpeed = m_GameNormaleSpeed;
        m_CurrentGameSpeed = GameSpeed.Normale;
        m_GameSlowedSpeed = 0.1f;
        m_GameSpeedChange = 1.4f;
        m_SlowTimeLeft = 5f;
    }

    private void Update()
    {
        if (m_CurrentGameSpeed == GameSpeed.Slowed)
        {
            if (m_GameSpeed > m_GameSlowedSpeed + 0.05f)
            {
                m_GameSpeed = Mathf.Lerp(m_GameSpeed, m_GameSlowedSpeed, m_GameSpeedChange * Time.deltaTime);
            }
            else
            {
                m_GameSpeed = m_GameSlowedSpeed;
            }

            m_SlowTimeLeft -= Time.deltaTime;
            if (m_SlowTimeLeft <= 0f)
            {
                SpeedUpTime();
            }
        }
        else if (m_CurrentGameSpeed == GameSpeed.Normale)
        {
            if (m_GameSpeed < m_GameNormaleSpeed - 0.05f)
            {
                m_GameSpeed = Mathf.Lerp(m_GameSpeed, m_GameNormaleSpeed, m_GameSpeedChange * Time.deltaTime);
            }
            else
            {
                m_GameSpeed = m_GameNormaleSpeed;
            }
        }
    }

    public void SwitchGameSpeed()
    {
        if (m_SlowTimeLeft > 0f)
        {
            if (m_CurrentGameSpeed == GameSpeed.Slowed)
            {
                m_CurrentGameSpeed = GameSpeed.Normale;
            }
            else if (m_CurrentGameSpeed == GameSpeed.Normale)
            {
                m_CurrentGameSpeed = GameSpeed.Slowed;
            }
        }
    }

    public void SlowDownTime()
    {
        if (m_SlowTimeLeft > 0f)
        {
            m_CurrentGameSpeed = GameSpeed.Slowed;
        }
    }

    public void SpeedUpTime()
    {
        m_CurrentGameSpeed = GameSpeed.Normale;
    }

    public float GetGameSpeed()
    {
        return m_GameSpeed;
    }
}

public enum GameSpeed
{
    Normale = 0,
    Slowed
}