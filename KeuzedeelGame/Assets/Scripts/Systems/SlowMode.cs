using UnityEngine;
using System.Collections.Generic;

public class SlowMode : MonoBehaviour
{
    private GameSpeed m_CurrentGameSpeed;
    [SerializeField]
    private List<GameObject> m_SlowMeters;
    [SerializeField]
    private List<GameObject> m_SlowMeterMaxPoint;
    [SerializeField]
    private List<GameObject> m_SLowMeterLowestPoint;
    [SerializeField]
    private GameObject m_SlowModeScreenEffect;
    private float m_SlowPrecentageLeft;

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
        m_SlowPrecentageLeft = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            SwitchGameSpeed();
        }

        if (m_CurrentGameSpeed == GameSpeed.Slowed)
        {
            m_SlowModeScreenEffect.SetActive(true);
            if (m_GameSpeed > m_GameSlowedSpeed + 0.05f)
            {
                m_GameSpeed = Mathf.Lerp(m_GameSpeed, m_GameSlowedSpeed, m_GameSpeedChange * Time.deltaTime);
            }
            else
            {
                m_GameSpeed = m_GameSlowedSpeed;
            }

            m_SlowTimeLeft -= Time.deltaTime;
            m_SlowPrecentageLeft = (100.0f * m_SlowTimeLeft / 5.0f) / 100.0f;
            m_SlowPrecentageLeft = Mathf.Clamp(m_SlowPrecentageLeft, 0.0f, 1.0f);
            for (int i = 0; i < m_SlowMeters.Count; i++)
            {
                Vector3 pos = Vector3.Lerp(m_SLowMeterLowestPoint[i].transform.position, m_SlowMeterMaxPoint[i].transform.position, m_SlowPrecentageLeft);
                m_SlowMeters[i].transform.position = pos;
            }

            if (m_SlowTimeLeft <= 0f)
            {
                SpeedUpTime();
            }
        }
        else if (m_CurrentGameSpeed == GameSpeed.Normale)
        {
            m_SlowTimeLeft += 0.5f * Time.deltaTime;
            m_SlowPrecentageLeft = (100.0f * m_SlowTimeLeft / 5.0f) / 100.0f;
            m_SlowPrecentageLeft = Mathf.Clamp(m_SlowPrecentageLeft, 0.0f, 1.0f);
            for (int i = 0; i < m_SlowMeters.Count; i++)
            {
                Vector3 pos = Vector3.Lerp(m_SLowMeterLowestPoint[i].transform.position, m_SlowMeterMaxPoint[i].transform.position, m_SlowPrecentageLeft);
                m_SlowMeters[i].transform.position = pos;
            }

            m_SlowModeScreenEffect.SetActive(false);
            if (m_GameSpeed < m_GameNormaleSpeed - 0.05f)
            {
                m_GameSpeed = Mathf.Lerp(m_GameSpeed, m_GameNormaleSpeed, m_GameSpeedChange * Time.deltaTime);
            }
            else
            {
                m_GameSpeed = m_GameNormaleSpeed;
            }
        }

        m_SlowTimeLeft = Mathf.Clamp(m_SlowTimeLeft, 0.0f, 5.0f);
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