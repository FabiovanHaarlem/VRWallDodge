using UnityEngine;
using UnityEngine.UI;

public class CalibratePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HeadSet;
    [SerializeField]
    private Text m_CalibrateStateText;
    [SerializeField]
    private Text m_CalibrateTimer;

    private CalibrationState m_CurrentState; 

    private float m_CalibartingProgress;
    private float m_CalibrateDelay;

    private bool m_Calibrating;
    private bool m_ProcessActive;

    private void Awake()
    {
        m_CurrentState = CalibrationState.ReadyToCalibrate;
        m_CalibrateDelay = 3f;
        m_CalibartingProgress = 0f;
        m_CalibrateStateText.text = "Press calibration";
    }

    private void Update()
    {
        if (m_Calibrating)
        {
            m_CalibrateDelay -= Time.deltaTime;
            m_CalibrateTimer.text = Mathf.Ceil(m_CalibrateDelay).ToString();
            transform.position = Vector3.Lerp(transform.position, new Vector3(
                transform.position.x, m_HeadSet.transform.position.y - 0.4f, transform.position.z), 1f * Time.deltaTime);

            if (m_CalibrateDelay <= 0f)
            {
                if (m_CalibartingProgress < 0.5f)
                {
                    m_CurrentState = CalibrationState.CalibratingHeight;
                    CalibratePlayerHeight();
                }
                else
                {
                    m_CurrentState = CalibrationState.CalibratingDuckHeight;
                    CalibratePlayerDuckHeight();
                }
            }
        }
    }

    private void ResetTimer()
    {
        m_CalibrateDelay = 3f;
    }

    private void CalibratePlayerHeight()
    {
        m_CalibartingProgress += 0.5f;
        PlayerData.SetPlayerHeight(m_HeadSet.transform.position.y);
        m_Calibrating = false;
        m_CurrentState = CalibrationState.WaitingForPlayerToBeReady;
        ActivateCalibarting();
        ResetTimer();
    }

    private void CalibratePlayerDuckHeight()
    {
        ResetTimer();
        PlayerData.SetPlayerDuckHeight(m_HeadSet.transform.position.y);
        m_CalibartingProgress += 0.5f;
        m_Calibrating = false;
        CalibratingComplete();
    }

    private void CalibratingComplete()
    {
        m_CurrentState = CalibrationState.ReadyToCalibrate;
        m_CalibrateStateText.text = "Height: " + System.Math.Round(PlayerData.GetPlayerHeight(), 1);
        m_CalibrateTimer.text = "Duck height: " + System.Math.Round(PlayerData.GetPlayerDuckHeightMax(), 1);
    }

    private void StartTimer()
    {
        m_Calibrating = true;
    }

    private void ActivateCalibarting()
    {
        m_CurrentState = CalibrationState.WaitingForPlayerToBeReady;
        m_CalibrateTimer.text = "Press the button when ready";
        if (m_CalibartingProgress < 0.5f)
        {
            m_CalibrateStateText.text = "Please stand straight";
        }
        else
        {
            m_CalibrateStateText.text = "Please press the button and duck as far as you are comfortable";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_CurrentState == CalibrationState.ReadyToCalibrate)
        {
            m_ProcessActive = true;
            m_CalibartingProgress = 0f;
            ActivateCalibarting();
        }
        else if (m_CurrentState == CalibrationState.WaitingForPlayerToBeReady)
        {
            StartTimer();
        }
    }

    private enum CalibrationState
    {
        ReadyToCalibrate = 0,
        WaitingForPlayerToBeReady,
        CalibratingHeight,
        CalibratingDuckHeight
    }
}
