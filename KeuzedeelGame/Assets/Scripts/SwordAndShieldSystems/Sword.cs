using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Sword;
    private ControllerInput m_ControllerInput;

    private void Start()
    {
        m_ControllerInput = GetComponent<ControllerInput>();
        m_ControllerInput.E_OnTriggerDownEvent += ShowLaser;
        m_ControllerInput.E_OnTriggerUpEvent += HideLaser;
    }

    private void ShowLaser()
    {
        m_Sword.SetActive(true);
    }

    private void HideLaser()
    {
        m_Sword.SetActive(false);
    }
}
