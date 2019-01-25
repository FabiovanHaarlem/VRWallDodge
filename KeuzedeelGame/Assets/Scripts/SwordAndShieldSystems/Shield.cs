using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Shield;
    private ControllerInput m_ControllerInput;

    private void Start()
    {
        m_ControllerInput = GetComponent<ControllerInput>();
        m_ControllerInput.E_OnTriggerDownEvent += ShowShield;
        m_ControllerInput.E_OnTriggerUpEvent += HideShield;
    }

    private void ShowShield()
    {
        m_Shield.SetActive(true);
    }

    private void HideShield()
    {
        m_Shield.SetActive(false);
    }
}
