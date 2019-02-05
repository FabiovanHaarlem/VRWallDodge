using System.Collections.Generic;
using UnityEngine;
using SimpleEasing;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_SelectedText;
    [SerializeField]
    private GameObject m_Screen;
    [SerializeField]
    private List<GameObject> m_Buttons;
    private float m_MoveTimer;
    [SerializeField]
    private float m_AnimationLength;
    [SerializeField]
    private float m_AnimationStartPositionOffset;
    [SerializeField]
    private float m_AnimationEndPositionOffset;

    private Vector3 m_DefaultPosition;

    private bool m_MoveUp;

    private GameObject m_ToActivateLevels;
    private GameObject m_ActiveLevels;

    private bool m_Switch;
    private int m_Index;

    private void Start()
    {
        for (int i = 0; i < m_Buttons.Count; i++)
        {
            m_Buttons[i].SetActive(false);
        }
    }

    public void SelectLevel(int index)
    {
        m_ToActivateLevels = m_Buttons[index];

        if (m_ActiveLevels == null)
        {
            m_ActiveLevels = m_ToActivateLevels;
            m_MoveUp = true;
            m_ActiveLevels.SetActive(true);
            m_Index = index;
        }
        else if (m_ActiveLevels == m_ToActivateLevels)
        {
            m_MoveUp = false;
        }
        else if (m_ActiveLevels != null)
        {
            m_MoveUp = false;
            m_Switch = true;
        }
    }

    private void Update()
    {
        float y = 0.0f;
        //y = Easing.easeInQuint(m_buttonTimer, m_animationStart, m_animationChange, m_animationLength);
        //y = Easing.easeOutQuint(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);
        y = Easing.easeInOutExpo(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);

        if (m_MoveUp)
            m_MoveTimer += Time.deltaTime;
        else
            m_MoveTimer -= Time.deltaTime;

        m_MoveTimer = Mathf.Clamp(m_MoveTimer, 0, m_AnimationLength);

        m_Screen.transform.position = m_DefaultPosition + new Vector3(0, y, 0);

        if (m_MoveTimer <= 0.0f && m_Switch)
        {
            SwitchLevels();
        }
    }

    private void SwitchLevels()
    {
        m_ActiveLevels.SetActive(false);
        m_ActiveLevels = m_ToActivateLevels;
        m_MoveUp = true;
        m_ActiveLevels.transform.parent = m_Screen.transform;
        m_Switch = false;
        m_ActiveLevels.SetActive(true);
    }
}
