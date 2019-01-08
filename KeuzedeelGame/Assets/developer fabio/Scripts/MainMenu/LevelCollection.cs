using System.Collections.Generic;
using UnityEngine;

public class LevelCollection : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UIButtons;
    [SerializeField]
    private Transform m_UIMoveUpPosition;
    [SerializeField]
    private Transform m_UIMoveDownPosition;
    [SerializeField]
    private LevelSelectManager m_SelectManager;

    private List<LevelCollection> m_LevelCollections;

    private Transform m_CurrentMoveTarget;

    private bool m_IsActive;

    public bool GetIfActive()
    {
        return m_IsActive;
    }

    private void Awake()
    {
        m_CurrentMoveTarget = m_UIMoveDownPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_SelectManager.SetLevelsActive(this);
        }
    }

    public void MoveMenuDown()
    {
        m_CurrentMoveTarget = m_UIMoveDownPosition;
    }

    public void MoveMenuUp()
    {
        m_CurrentMoveTarget = m_UIMoveUpPosition;
        m_IsActive = false;
    }

    private void Halfway()
    {
        m_IsActive = true;
    }

    public void Move()
    {
        m_UIButtons.transform.position = Vector3.Lerp(m_UIButtons.transform.position, m_CurrentMoveTarget.position, 3f * Time.deltaTime);

        if (Vector3.Distance(m_UIButtons.transform.position, m_UIMoveDownPosition.position) < 0.05f)
        {
            if (m_IsActive)
            {
                m_SelectManager.MenuCanSwitch();
            }
            m_UIButtons.gameObject.SetActive(false);
        }
        else
        {
            m_UIButtons.gameObject.SetActive(true);
        }

        if (Vector3.Distance(m_UIButtons.transform.position, m_UIMoveDownPosition.position) > 0.05f)
        {
            Halfway();
        }
    }
}
