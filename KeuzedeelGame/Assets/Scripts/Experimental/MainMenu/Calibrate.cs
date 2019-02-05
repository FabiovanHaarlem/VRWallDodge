using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Calibrate : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HeightLine;
    [SerializeField]
    private TextMeshProUGUI m_HeightNumber;
    [SerializeField]
    private GameObject m_DuckHeightLine;
    [SerializeField]
    private TextMeshProUGUI m_DuckHeightNumber;
    [SerializeField]
    private TextMeshProUGUI m_InformationText;
    [SerializeField]
    private GameObject m_PlayerHead;
    private CalibrationState m_CurrentState;

    private float m_PlayerHeight;
    private float m_PlayerDuckHeight;

    private void Awake()
    {
        //m_PlayerHead = GameObject.Find("Camera (head)");
        m_CurrentState = CalibrationState.ReadyToCalibrateHeight;
        m_HeightLine.SetActive(false);
        m_DuckHeightLine.SetActive(false);
        m_InformationText.text = "(PRESS HERE)" + Environment.NewLine + "Start height check";
    }

    public void ButtonPressed()
    {
        switch(m_CurrentState)
        {
            case CalibrationState.ReadyToCalibrateHeight:
                m_DuckHeightLine.SetActive(false);
                m_CurrentState = CalibrationState.GettingPlayerHeight;
                m_InformationText.text = "(PRESS HERE)" + Environment.NewLine + "Lock in height";
                m_HeightLine.SetActive(true);
                break;
            case CalibrationState.GettingPlayerHeight:
                SetHeight();
                m_CurrentState = CalibrationState.GettingPlayerDuckHeight;
                m_DuckHeightLine.SetActive(true);
                m_InformationText.text = "(PRESS HERE)" + Environment.NewLine + "Lock in duck height";
                break;
            case CalibrationState.GettingPlayerDuckHeight:
                SetDuckHeight();
                m_CurrentState = CalibrationState.Finishing;
                SetPlayerHeightProfile();
                m_InformationText.text = "(PRESS HERE)" + Environment.NewLine + "Restart height check";
                break;
            case CalibrationState.Finishing:
                m_CurrentState = CalibrationState.ReadyToCalibrateHeight;
                break;
        }
    }

    public void Cancle()
    {
        m_CurrentState = CalibrationState.ReadyToCalibrateHeight;
        m_HeightLine.SetActive(false);
        m_DuckHeightLine.SetActive(false);
        m_InformationText.text = "(PRESS HERE)" + Environment.NewLine + "Start height check";
    }

    private void SetHeight()
    {
        m_PlayerHeight = m_PlayerHead.transform.position.y;
    }

    private void SetDuckHeight()
    {
        m_PlayerDuckHeight = m_PlayerHead.transform.position.y;
    }

    private void SetPlayerHeightProfile()
    {
        PlayerData.SetPlayerHeight(m_PlayerHeight);
        PlayerData.SetPlayerDuckHeight(m_PlayerDuckHeight);
        m_CurrentState = CalibrationState.ReadyToCalibrateHeight;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ButtonPressed();
        }

        if (m_CurrentState == CalibrationState.GettingPlayerHeight)
        {
            Vector3 playerHeight = new Vector3(m_HeightLine.transform.position.x, m_PlayerHead.transform.position.y, m_HeightLine.transform.position.z);
            m_HeightLine.transform.position = Vector3.MoveTowards(m_HeightLine.transform.position, playerHeight, 2.0f * Time.deltaTime);
            UpdateHeightText();
        }
        else if (m_CurrentState == CalibrationState.GettingPlayerDuckHeight)
        {
            Vector3 playerHeight = new Vector3(m_DuckHeightLine.transform.position.x, m_PlayerHead.transform.position.y, m_DuckHeightLine.transform.position.z);
            m_DuckHeightLine.transform.position = Vector3.MoveTowards(m_DuckHeightLine.transform.position, playerHeight, 2.0f * Time.deltaTime);
            UpdateDuckHeightText();
        }
    }

    private void UpdateHeightText()
    {
        m_HeightNumber.text = "Height: " + System.Math.Round(m_PlayerHead.transform.position.y, 1);
    }

    private void UpdateDuckHeightText()
    {
        m_DuckHeightNumber.text = "Duck height: " + System.Math.Round(m_PlayerHead.transform.position.y, 1);
    }

    private enum CalibrationState
    {
        ReadyToCalibrateHeight = 0,
        GettingPlayerHeight,
        GettingPlayerDuckHeight,
        Finishing
    }
}
