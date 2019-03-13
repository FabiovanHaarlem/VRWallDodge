using System.Collections.Generic;
using UnityEngine;
using SimpleEasing;

public class LevelCollection : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UIButtons;
    [SerializeField]
    private LevelSelectManager m_SelectManager;

    private List<LevelCollection> m_LevelCollections;

    private bool m_IsActive;

    private float m_MoveTimer;
    [SerializeField]
    private float m_AnimationLength;
    [SerializeField]
    private float m_AnimationStartPositionOffset;
    [SerializeField]
    private float m_AnimationEndPositionOffset;
    private Vector3 m_DefaultPosition;
    private bool m_MoveUp;

    public bool GetIfActive()
    {
        return m_IsActive;
    }

    private void Awake()
    {
        m_MoveUp = false;

        m_MoveTimer = 0.0f;
        m_DefaultPosition = m_UIButtons.transform.position;
    }

    public void MoveMenuDown()
    {
        m_MoveUp = false;
    }

    public void MoveMenuUp()
    {
        m_IsActive = false;
        m_MoveUp = true;
    }

    private void Halfway()
    {
        m_IsActive = true;
    }

    public void Move()
    {
        if (m_MoveTimer <= 0.1f)
        {
            if (m_IsActive)
            {
                m_SelectManager.MenuCanSwitch();
            }
        }

        if (m_MoveTimer >= 0.5f)
        {
            Halfway();
        }

        float y = 0.0f;
        //y = Easing.easeInQuint(m_buttonTimer, m_animationStart, m_animationChange, m_animationLength);
        //y = Easing.easeOutQuint(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);
        y = Easing.easeInOutExpo(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);

        if (m_MoveUp)
            m_MoveTimer += Time.deltaTime;
        else
            m_MoveTimer -= Time.deltaTime;

        m_MoveTimer = Mathf.Clamp(m_MoveTimer, 0, m_AnimationLength);

        m_UIButtons.transform.position = m_DefaultPosition + new Vector3(0, y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_SelectManager.SetLevelsActive(this);
        }
    }

}
