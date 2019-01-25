using UnityEngine;
using UnityEngine.UI;

public class Calibrate : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HeightLine;
    [SerializeField]
    private Text m_HeightNumber;
    [SerializeField]
    private GameObject m_DuckHeightLine;
    [SerializeField]
    private Text m_DuckHeightNumber;

    private GameObject m_PlayerHead;
    private CalibrationState m_CurrentState;

    private float m_PlayerHeight;
    private float m_PlayerDuckHeight;

    private void Awake()
    {
        m_PlayerHead = GameObject.Find("Camera (head)");
        m_CurrentState = CalibrationState.ReadyToCalibrateHeight;
    }

    public void ButtonPressed()
    {
        switch(m_CurrentState)
        {
            case CalibrationState.ReadyToCalibrateHeight:
                break;
            case CalibrationState.GettinPlayerHeight:
                break;
            case CalibrationState.ReadyToCalibrateDuckHeight:
                break;
            case CalibrationState.GettinPlayerDuckHeight:
                break;
            case CalibrationState.Finishing:
                break;
        }
    }

    private void Update()
    {
        Vector3 playerHeight = new Vector3(transform.position.x, m_PlayerHead.transform.position.y, transform.position.z);
        if (m_CurrentState == CalibrationState.GettinPlayerHeight)
        {
            m_HeightLine.transform.position = Vector3.MoveTowards(m_HeightLine.transform.position, playerHeight, 3.0f * Time.deltaTime);
        }
        else if (m_CurrentState == CalibrationState.GettinPlayerHeight)
        {
            m_DuckHeightLine.transform.position = Vector3.MoveTowards(m_DuckHeightLine.transform.position, playerHeight, 3.0f * Time.deltaTime);
        }
    }

    private void UpdateHeightText()
    {
        m_HeightNumber.text = "Height: " + m_PlayerHead.transform.position.y;
    }

    private void UpdateDuckHeightText()
    {
        m_DuckHeightNumber.text = "Duck height: " + m_PlayerHead.transform.position.y;
    }

    private enum CalibrationState
    {
        ReadyToCalibrateHeight = 0,
        GettinPlayerHeight,
        ReadyToCalibrateDuckHeight,
        GettinPlayerDuckHeight,
        Finishing
    }
}
